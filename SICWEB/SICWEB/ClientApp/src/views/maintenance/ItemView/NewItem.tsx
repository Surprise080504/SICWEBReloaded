import { useEffect, useState } from "react";
import type { FC } from "react";
import PropTypes from "prop-types";

import _ from "lodash";
import * as Yup from "yup";
import { Formik } from "formik";
import {
  Box,
  Typography,
  TextField,
  Button,
  IconButton,
  Divider,
  FormHelperText,
  makeStyles,
  Grid,
  Dialog,
} from "@material-ui/core";
import AddIcon2 from "@material-ui/icons/Add";
import type { Theme } from "src/theme";
import type { Event } from "src/types/calendar";
import NewCategory from "./NewCategory";
import {
  getFamilyAndSub,
  getProducts,
  getProduct,
  getSubFamilies,
  saveItem,
  checkImageFile,
} from "src/apis/itemApi";
import { useSnackbar } from "notistack";
import useSettings from "src/hooks/useSettings";

interface NewItemProps {
  editID: number;
  _initialValue?: any;
  segments?: any[];
  products?: any[];
  families?: any[];
  subFamilies?: any[];
  familyperm?: boolean;
  subfamilyperm?: boolean;
  units?: any[];
  imagefile: any;
  event?: Event;
  _getInitialData?: () => void;
  onAddComplete?: () => void;
  onCancel?: () => void;
  onDeleteComplete?: () => void;
  onEditComplete?: () => void;
}

const useStyles = makeStyles((theme: Theme) => ({
  root: {},
  confirmButton: {
    marginLeft: theme.spacing(2),
  },
}));

const NewItem: FC<NewItemProps> = ({
  editID,
  _initialValue,
  segments,
  products,
  families,
  familyperm,
  subfamilyperm,
  units,
  imagefile,
  event,
  _getInitialData,
  onAddComplete,
  onCancel,
  onDeleteComplete,
  onEditComplete,
}) => {
  const classes = useStyles();
  const { enqueueSnackbar } = useSnackbar();
  const { saveSettings } = useSettings();
  const [isModalOpen3, setIsModalOpen3] = useState(false);
  const [subFamilies, setSubFamilies] = useState<any>([]);

  const [family2, setFamily2] = useState<any>([]);
  const [subFamily2, setSubFamily2] = useState<any>([]);
  const [modalState, setModalState] = useState(0);
  const [sProducts, setSProducts] = useState<any>([]);

  const [selectedImage, setSelectedImage] = useState<any>();
  const [selectedImageUrl, setSelectedImageUrl] = useState<string>();

  const _checkUserImage = async () => {
    if (imagefile != null) {
      const checkImage = await checkImageFile(imagefile);

      if (checkImage) {
        setSelectedImageUrl("/uploads/" + imagefile);
      } else {
        setSelectedImageUrl("/uploads/thumb.png");
      }

      setSelectedImage(true);
    } else {
      setSelectedImageUrl("/uploads/thumb.png");
      setSelectedImage(false);
    }
  };

  const _getSubFamilies = (family) => {
    getSubFamilies(family).then((res) => {
      setSubFamilies(res);
      setSubFamily2(res);
    });
  };

  const _getProducto = (subfamily) => {
    getProduct(subfamily).then((res) => {
      setSProducts(res);
    });
  };

  const _getFamilyAndSub = () => {
    getFamilyAndSub(-1)
      .then((res) => {
        setFamily2(res["family"]);
        //  setSubFamily2(res["subFamily"]);
      })
      .catch((err) => {
        setFamily2([]);
        setSubFamily2([]);
      });
  };
  const handleModalClose3 = (): void => {
    setIsModalOpen3(false);
  };

  const handleModalOpen3 = (): void => {
    setIsModalOpen3(true);
  };

  const getInitialValues = () => {
    let _imagedata = "/uploads/" + imagefile;

    if (editID > -1) {
      return _.merge(
        {},
        {
          id: -1,
          code: "",
          description: "",
          unit: -1,
          purchaseprice: "",
          saleprice: "",
          pid: -1,
          family: -1,
          subfamily: -1,
          image: null,
          imageUrl: null,
          submit: null,
        },
        {
          id: _initialValue[editID].itm_c_iid,
          code: _initialValue[editID].itm_c_ccodigo,
          description: _initialValue[editID].itm_c_vdescripcion,
          unit: _initialValue[editID].und_c_yid,
          purchaseprice: _initialValue[editID].itm_c_dprecio_compra,
          saleprice: _initialValue[editID].itm_c_dprecio_venta,
          pid: _initialValue[editID].pro_partida_c_iid,
          family: _initialValue[editID].ifm_c_iid,
          subfamily: _initialValue[editID].isf_c_iid,
          image: _imagedata,
          imageUrl: _imagedata,
          submit: null,
        }
      );
    } else {
      return {
        id: -1,
        code: "",
        description: "",
        unit: -1,
        purchaseprice: "",
        saleprice: "",
        pid: -1,
        family: -1,
        subfamily: -1,
        image: null,
        imageUrl: null,
        submit: null,
      };
    }
  };

  useEffect(() => {
    //editID > -1 && _getFamilyAndSub(_initialValue[editID].pro_partida_c_iid);
    _getFamilyAndSub();
    setSProducts([]);
    if (editID > -1) {
      _getSubFamilies(_initialValue[editID]?.ifm_c_iid);
      _getProducto(_initialValue[editID]?.isf_c_iid);
    }

    _checkUserImage();
  }, []);

  return (
    <>
      <Formik
        initialValues={getInitialValues()}
        validationSchema={Yup.object().shape({
          code: Yup.string()
            .max(200, "Debe tener 200 caracteres como máximo")
            .required("Se requiere el código"),
          description: Yup.string()
            .max(200, "Debe tener 200 caracteres como máximo")
            .required("Se requiere una descripción"),
          unit: Yup.number().min(0).required(),
          purchaseprice: Yup.number().required(
            "Se requiere el precio de compra"
          ),
          saleprice: Yup.number().required("Se requiere el Precio de Venta"),
          pid: Yup.number().min(0).required(),
          image: Yup.mixed().required("Este campo es requerido."),
        })}
        onSubmit={async (
          values,
          { resetForm, setErrors, setStatus, setSubmitting }
        ) => {
          saveSettings({ saving: true });
          if (selectedImageUrl == "/uploads/thumb.png") {
            saveSettings({ saving: false });
            enqueueSnackbar("por favor seleccione la imagen.", {
              variant: "error",
            });
            return;
          }
          window.setTimeout(() => {
            saveItem(values)
              .then((res) => {
                saveSettings({ saving: false });
                _getInitialData();
                enqueueSnackbar("Tus datos se han guardado exitosamente.", {
                  variant: "success",
                });
                resetForm();
                setStatus({ success: true });
                setSubmitting(false);
                onCancel();
              })
              .catch((err) => {
                _getInitialData();
                enqueueSnackbar("No se pudo guardar.", {
                  variant: "error",
                });
                saveSettings({ saving: false });
              });
          }, 1000);
        }}
      >
        {({
          errors,
          handleBlur,
          handleChange,
          handleSubmit,
          isSubmitting,
          setFieldTouched,
          setFieldValue,
          touched,
          values,
        }) => (
          <form onSubmit={handleSubmit}>
            <Box p={3}>
              <Typography
                align="center"
                gutterBottom
                variant="h4"
                color="textPrimary"
              >
                {editID > -1 ? "Editar ítem" : "Nuevo Item"}
              </Typography>
            </Box>
            <Divider />
            <Box p={3}>
              <Grid container spacing={3}>
                <Grid item lg={6} sm={6} xs={12}>
                  <TextField
                    size="small"
                    error={Boolean(touched.code && errors.code)}
                    fullWidth
                    helperText={touched.code && errors.code}
                    label={
                      <label>
                        Código <span style={{ color: "red" }}>*</span>
                      </label>
                    }
                    name="code"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    value={values.code}
                    variant="outlined"
                    InputLabelProps={{
                      shrink: true,
                    }}
                  />
                </Grid>
                <Grid item lg={6} sm={6} xs={12} style={{ display: "flex" }}>
                  <TextField
                    size="small"
                    error={Boolean(touched.unit && errors.unit)}
                    helperText={
                      touched.unit &&
                      errors.unit &&
                      "Se requiere unidad de medida"
                    }
                    label={
                      <label>
                        Unidad de Medida <span style={{ color: "red" }}>*</span>
                      </label>
                    }
                    name="unit"
                    fullWidth
                    SelectProps={{ native: true }}
                    select
                    variant="outlined"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    value={values.unit}
                    InputLabelProps={{
                      shrink: true,
                    }}
                  >
                    <option defaultValue="-1" key="-1" value="-1">
                      {"-- Seleccionar --"}
                    </option>
                    {units.map((unit) => (
                      <option key={unit.und_c_yid} value={unit.und_c_yid}>
                        {unit.und_c_vdesc}
                      </option>
                    ))}
                  </TextField>
                  <IconButton
                    size="small"
                    color="secondary"
                    aria-label="add to shopping cart"
                    onClick={() => {
                      setModalState(0);
                      handleModalOpen3();
                    }}
                  >
                    <AddIcon2 />
                  </IconButton>
                </Grid>
              </Grid>
              <Grid container spacing={3}>
                <Grid item lg={12} sm={12} xs={12}>
                  <TextField
                    size="small"
                    error={Boolean(touched.description && errors.description)}
                    fullWidth
                    helperText={touched.description && errors.description}
                    label={
                      <label>
                        Descripción <span style={{ color: "red" }}>*</span>
                      </label>
                    }
                    name="description"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    value={values.description}
                    variant="outlined"
                    InputLabelProps={{
                      shrink: true,
                    }}
                  />
                </Grid>
              </Grid>
              <Grid container spacing={3}>
                <Grid item lg={6} sm={6} xs={12}>
                  <TextField
                    size="small"
                    error={Boolean(
                      touched.purchaseprice && errors.purchaseprice
                    )}
                    fullWidth
                    helperText={
                      touched.purchaseprice &&
                      errors.purchaseprice &&
                      "El precio de compra debe ser un número"
                    }
                    label={
                      <label>
                        Precio de Compra <span style={{ color: "red" }}>*</span>
                      </label>
                    }
                    name="purchaseprice"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    value={values.purchaseprice}
                    variant="outlined"
                    InputLabelProps={{
                      shrink: true,
                    }}
                  />
                </Grid>
                <Grid item lg={6} sm={6} xs={12}>
                  <TextField
                    size="small"
                    error={Boolean(touched.saleprice && errors.saleprice)}
                    fullWidth
                    helperText={
                      touched.saleprice &&
                      errors.saleprice &&
                      "El precio de venta debe ser un número"
                    }
                    label={
                      <label>
                        Precio de Venta <span style={{ color: "red" }}>*</span>
                      </label>
                    }
                    name="saleprice"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    value={values.saleprice}
                    variant="outlined"
                    InputLabelProps={{
                      shrink: true,
                    }}
                  />
                </Grid>
              </Grid>
              <Grid container spacing={3}>
                <Grid item lg={6} sm={6} xs={12} style={{ display: "flex" }}>
                  <TextField
                    size="small"
                    label={
                      <label>
                        Familia <span style={{ color: "red" }}>*</span>
                      </label>
                    }
                    name="family"
                    //disabled
                    error={Boolean(touched.family && errors.family)}
                    helperText={
                      touched.family &&
                      errors.family &&
                      "Se requiere el familia"
                    }
                    fullWidth
                    SelectProps={{ native: true }}
                    select
                    onBlur={handleBlur}
                    onChange={(e) => {
                      _getSubFamilies(e.target.value);
                      setSProducts([]);
                      handleChange(e);
                    }}
                    value={values.family}
                    variant="outlined"
                    InputLabelProps={{
                      shrink: true,
                    }}
                  >
                    {
                      <option key="-1" value="-1">
                        {" "}
                        -- Seleccionar --{" "}
                      </option>
                    }
                    {family2.map((family) => (
                      <option
                        selected
                        key={family.ifm_c_iid}
                        value={family.ifm_c_iid}
                      >
                        {family.ifm_c_des}
                      </option>
                    ))}
                  </TextField>
                  <IconButton
                    hidden
                    size="small"
                    color="secondary"
                    aria-label="add to shopping cart"
                    disabled={familyperm}
                    onClick={() => {
                      setModalState(1);
                      handleModalOpen3();
                    }}
                  >
                    <AddIcon2 />
                  </IconButton>
                </Grid>
                <Grid item lg={6} sm={6} xs={12} style={{ display: "flex" }}>
                  <TextField
                    size="small"
                    label={
                      <label>
                        SubFamilia <span style={{ color: "red" }}>*</span>
                      </label>
                    }
                    name="subfamily"
                    //disabled
                    error={Boolean(touched.subfamily && errors.subfamily)}
                    helperText={
                      touched.subfamily &&
                      errors.subfamily &&
                      "Se requiere el subFamilia"
                    }
                    fullWidth
                    SelectProps={{ native: true }}
                    select
                    variant="outlined"
                    onBlur={handleBlur}
                    onChange={(e) => {
                      _getProducto(e.target.value);
                      handleChange(e);
                    }}
                    value={values.subfamily}
                    InputLabelProps={{
                      shrink: true,
                    }}
                  >
                    {
                      <option key="-1" value="-1">
                        {" "}
                        -- Seleccionar --{" "}
                      </option>
                    }
                    {subFamily2.map((subFamily) => (
                      <option
                        selected
                        key={subFamily.isf_c_iid}
                        value={subFamily.isf_c_iid}
                      >
                        {subFamily.isf_c_vdesc}
                      </option>
                    ))}
                  </TextField>
                  {/*JRDC*/}
                  <IconButton
                    size="small"
                    color="secondary"
                    aria-label="add to shopping cart"
                    disabled={subfamilyperm}
                    onClick={() => {
                      setModalState(2);
                      handleModalOpen3();
                    }}
                  >
                    <AddIcon2 />
                  </IconButton>
                </Grid>
              </Grid>
              <Grid container spacing={3}>
                <Grid item lg={6} sm={6} xs={12} style={{ display: "flex" }}>
                  <TextField
                    size="small"
                    label={
                      <label>
                        Producto Partida <span style={{ color: "red" }}>*</span>
                      </label>
                    }
                    name="pid"
                    error={Boolean(touched.pid && errors.pid)}
                    helperText={
                      touched.pid && errors.pid && "Se requiere el producto"
                    }
                    fullWidth
                    SelectProps={{ native: true }}
                    select
                    onBlur={handleBlur}
                    onChange={(e) => {
                      handleChange(e);
                    }}
                    value={values.pid}
                    variant="outlined"
                    InputLabelProps={{
                      shrink: true,
                    }}
                  >
                    <option defaultValue="-1" key="-1" value="-1">
                      {"-- Seleccionar --"}
                    </option>
                    {sProducts.map((product) => (
                      <option
                        key={product.pro_partida_c_iid}
                        value={product.pro_partida_c_iid}
                      >
                        {product.pro_partida_c_vdescripcion}
                      </option>
                    ))}
                  </TextField>
                  {/*JRDC*/}
                  <IconButton
                    size="small"
                    color="secondary"
                    aria-label="add to shopping cart"
                    onClick={() => handleModalOpen3()}
                  >
                    <AddIcon2 />
                  </IconButton>
                </Grid>
                <Grid item lg={6} sm={6} xs={12} style={{ display: "flex" }}>
                  <></>
                </Grid>
              </Grid>
              <Grid
                item
                xl={6}
                lg={6}
                md={6}
                sm={12}
                xs={12}
                style={{ margin: "auto", textAlign: "center" }}
              >
                <Grid container spacing={3}>
                  <Grid item xl={12} xs={12}>
                    <label
                      style={{
                        fontSize: 15,
                        color: "rgba(0, 0, 0, 0.54)",
                      }}
                    >
                      &nbsp;&nbsp;&nbsp;&nbsp;Image
                      <span style={{ color: "red" }}>*</span>
                    </label>
                  </Grid>
                  <Grid item xl={12} lg={12} md={12} sm={12} xs={12}>
                    <div style={{ padding: "10%" }}>
                      <div
                        style={{
                          minHeight: "50px",
                          position: "relative",
                          textAlign: "center",
                          border: Boolean(touched.image && errors.image)
                            ? "1px solid red"
                            : null,
                        }}
                      >
                        <input
                          type="file"
                          name="image"
                          style={{
                            width: "100%",
                            height: "100%",
                            background: "red",
                            left: 0,
                            top: 0,
                            position: "absolute",
                            opacity: 0,
                            zIndex: 99999,
                          }}
                          onChange={(e) => {
                            if (e.target.files && e.target.files.length > 0) {
                              setSelectedImage(e.target.files[0]);
                              setSelectedImageUrl(
                                URL.createObjectURL(e.target.files[0])
                              );
                            }

                            setFieldValue(
                              "imageUrl",
                              URL.createObjectURL(e.currentTarget.files[0])
                            );
                            setFieldValue("image", e.currentTarget.files[0]);

                            setFieldValue("imageChange", true);
                          }}
                        />
                        {/* <Field /> */}
                        <img
                          src={selectedImageUrl}
                          style={{
                            width: 250,
                            maxHeight: 250,
                          }}
                          alt=""
                        />
                      </div>
                    </div>
                    {Boolean(touched.image && errors.image) && (
                      <div>
                        <label>
                          <span style={{ color: "red" }}>
                            {touched.image && errors.image}
                          </span>
                        </label>
                      </div>
                    )}
                  </Grid>
                </Grid>
              </Grid>
            </Box>
            <Divider />
            {errors.submit && (
              <Box mt={3}>
                <FormHelperText error>{errors.submit}</FormHelperText>
              </Box>
            )}
            <Box p={2} display="flex" alignItems="center">
              <Box flexGrow={1} />
              <Button onClick={onCancel}>{"Cancelar"}</Button>
              <Button
                variant="contained"
                type="submit"
                disabled={isSubmitting}
                color="secondary"
                className={classes.confirmButton}
              >
                {"Confirmar"}
              </Button>
            </Box>
          </form>
        )}
      </Formik>
      <Dialog
        maxWidth="sm"
        fullWidth
        onClose={handleModalClose3}
        open={isModalOpen3}
      >
        {isModalOpen3 && (
          <NewCategory
            modalState={modalState}
            segments={segments}
            families={families}
            subFamilies={subFamilies}
            units={units}
            _getInitialData={_getInitialData}
            onCancel={handleModalClose3}
          />
        )}
      </Dialog>
    </>
  );
};

NewItem.propTypes = {
  // @ts-ignore
  event: PropTypes.object,
  onAddComplete: PropTypes.func,
  onCancel: PropTypes.func,
  onDeleteComplete: PropTypes.func,
  onEditComplete: PropTypes.func,
  // @ts-ignore
  range: PropTypes.object,
};

export default NewItem;
