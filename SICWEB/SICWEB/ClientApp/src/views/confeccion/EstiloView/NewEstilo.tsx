import { useEffect, useState } from "react";
import type { FC } from "react";

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
  FormGroup,
  FormControlLabel,
  Checkbox,
} from "@material-ui/core";
import AddIcon2 from "@material-ui/icons/Add";
import type { Theme } from "src/theme";
import NewCategory from "./NewCategory";
import {
  getBrands,
  getCategory,
  getColor,
  getColors,
  getImage,
  getOriginCategories,
  getTallas,
  saveStyle,
} from "src/apis/styleApi";
import { useSnackbar } from "notistack";
import useSettings from "src/hooks/useSettings";
import FileCopyIcon from "@material-ui/icons/FileCopy";
import { getItem } from "src/apis/itemApi";

interface NewEstiloProps {
  editID: number;
  _initialValue?: any;
  onCancel?: () => void;
  handleSearch?: () => void;
  _getInitialData?: () => void;
}

const useStyles = makeStyles((theme: Theme) => ({
  root: {},
  confirmButton: {
    marginLeft: theme.spacing(2),
  },
}));

const NewEstilo: FC<NewEstiloProps> = ({
  editID,
  _initialValue,
  onCancel,
  handleSearch,
  _getInitialData,
}) => {
  const classes = useStyles();
  const { enqueueSnackbar } = useSnackbar();
  const { saveSettings } = useSettings();

  const [brands, setBrands] = useState<any>([]);
  const [originColors, setOriginColors] = useState<any>([]);
  const [colors, setColors] = useState<any>([]);
  const [categories, setCategories] = useState<any>([]);
  const [originCategories, setOriginCategories] = useState<any>([]);
  const [tallas, setTallas] = useState<any>([]);
  const [items, setItems] = useState<any>([]);
  const [isModalOpen3, setIsModalOpen3] = useState(false);
  const [modalState, setModalState] = useState(0);

  const [fValues, setFValues] = useState(null);

  const [ready, setReady] = useState(false);
  const [imageData, setImageData] = useState(null);

  useEffect(() => {
    setDefaultData();
  }, []);

  useEffect(() => {
    imageData! == null &&
      window.setTimeout(() => {
        setReady(true);
      }, 300);
  }, [imageData]);

  const setDefaultData = () => {
    getBrands()
      .then((res) => {
        setBrands(res);
      })
      .catch((err) => {
        setBrands([]);
      });
    getColors()
      .then((res) => {
        setOriginColors(res);
      })
      .catch((err) => {
        setOriginColors([]);
      });
    getOriginCategories()
      .then((res) => {
        setOriginCategories(res);
      })
      .catch((err) => {
        setOriginCategories([]);
      });
    getTallas()
      .then((res) => {
        setTallas(res);
      })
      .catch((err) => {
        setTallas([]);
      });
    getItem({
      code: "",
      description: "",
      family: "-1",
      subFamily: "-1",
    })
      .then((res) => {
        setItems(res);
      })
      .catch((err) => {
        setItems([]);
      });
    if (editID > -1) {
      const _i = _get(editID);

      getImage(_i.estilo_c_iid).then((res) => {
        setImageData(res);
      });
      getColor({ id: _i.marca_c_vid })
        .then((res) => {
          setColors(res);
        })
        .catch((err) => {
          setColors([]);
        });
      getCategory({ id: _i.marca_c_vid })
        .then((res) => {
          setCategories(res);
        })
        .catch((err) => {
          setCategories([]);
        });
    } else {
      getImage(editID).then((res) => {
        setImageData(res);
      });
      getColor({ id: fValues })
        .then((res) => {
          setColors(res);
        })
        .catch((err) => {
          setColors([]);
        });
      getCategory({ id: fValues })
        .then((res) => {
          setCategories(res);
        })
        .catch((err) => {
          setCategories([]);
        });
    }
  };

  const handleModalClose3 = (): void => {
    setIsModalOpen3(false);
  };

  const handleModalOpen3 = (): void => {
    setIsModalOpen3(true);
  };

  const _get = (id) => {
    return _initialValue[id];
  };

  const getChecked = (tid, sizes) => {
    for (var i = 0; i < sizes.length; i++) {
      if (sizes[i].key === tid) return true;
    }
    return false;
  };

  const getInitialValues = () => {
    if (editID > -1) {
      const _i = _get(editID);

      var _sizes = [];
      for (var i = 0; i < tallas.length; i++) {
        _sizes.push({
          id: i,
          key: tallas[i].talla_c_vid,
          description: tallas[i].talla_c_vdescripcion,
          check: getChecked(tallas[i].talla_c_vid, _i.sizeName),
        });
      }

      return _.merge(
        {},
        {
          id: "-1",
          code: "",
          name: "",
          description: "",
          item: -1,
          image: null,
          imageUrl: null,
          brand: "-1",
          color: "-1",
          category: "-1",
          sizes: "-1",
          imageChange: false,
          submit: null,
        },
        {
          id: _i.estilo_c_iid,
          code: _i.estilo_c_vcodigo,
          name: _i.estilo_c_vnombre,
          description: _i.estilo_c_vdescripcion,
          item: _i.itm_c_iid,
          image: imageData,
          imageUrl: imageData,
          brand: _i.marca_c_vid,
          color: _i.marca_color_c_vid,
          category: _i.marca_categoria_c_vid,
          sizes: _sizes,
          imageChange: false,
          submit: null,
        }
      );
    } else {
      var sizes = [];
      for (var ii = 0; ii < tallas.length; ii++) {
        sizes.push({
          id: i,
          key: tallas[ii].talla_c_vid,
          description: tallas[ii].talla_c_vdescripcion,
          check: false,
        });
      }

      return {
        id: "-1",
        code: "",
        name: "",
        description: "",
        item: "-1",
        image: null,
        imageUrl: imageData,
        brand: "-1",
        color: "-1",
        category: "-1",
        sizes: sizes,
        imageChange: false,
        submit: null,
      };
    }
  };

  return (
    <>
      {ready && (
        <>
          <Formik
            initialValues={getInitialValues()}
            validationSchema={Yup.object().shape({
              code: Yup.string()
                .max(20, "Debe tener 20 caracteres como máximo")
                .required("Este campo es requerido."),
              name: Yup.string()
                .max(50, "Debe tener 50 caracteres como máximo")
                .required("Este campo es requerido."),
              description: Yup.string().max(
                200,
                "Debe tener 50 caracteres como máximo"
              ),
              item: Yup.mixed().notOneOf(["-1"], "Este campo es requerido."),
              brand: Yup.mixed().notOneOf(["-1"], "Este campo es requerido."),
              color: Yup.mixed().notOneOf(["-1"], "Este campo es requerido."),
              category: Yup.mixed().notOneOf(
                ["-1"],
                "Este campo es requerido."
              ),
              size: Yup.mixed().notOneOf(["-1"], "Este campo es requerido."),
              image: Yup.mixed().required("Este campo es requerido."),
            })}
            onSubmit={async (
              values,
              { resetForm, setErrors, setStatus, setSubmitting }
            ) => {
              saveSettings({ saving: true });
              window.setTimeout(() => {
                saveStyle(values)
                  .then((res) => {
                    saveSettings({ saving: false });
                    // _getInitialData();
                    enqueueSnackbar("Tus datos se han guardado exitosamente.", {
                      variant: "success",
                    });
                    resetForm();
                    setStatus({ success: true });
                    setSubmitting(false);
                    handleSearch();
                    onCancel();
                  })
                  .catch((err) => {
                    // _getInitialData();
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
                    {editID > -1 ? "Editar ESTILO" : "Nuevo ESTILO"}
                  </Typography>
                </Box>
                <Divider />
                <Box p={3}>
                  <Grid container spacing={3}>
                    <Grid item xl={6} lg={6} md={6} sm={12} xs={12}>
                      <Grid container spacing={3}>
                        <Grid item xl={12} xs={12}>
                          <label
                            style={{
                              fontSize: 15,
                              color: "rgba(0, 0, 0, 0.54)",
                            }}
                          >
                            &nbsp;&nbsp;&nbsp;&nbsp;Datos del Estilo
                          </label>
                        </Grid>
                        <Grid item xl={12} lg={12} md={12} sm={12} xs={12}>
                          <TextField
                            size="small"
                            fullWidth
                            label={
                              <label>
                                Código <span style={{ color: "red" }}>*</span>
                              </label>
                            }
                            InputLabelProps={{
                              shrink: true,
                            }}
                            variant="outlined"
                            InputProps={{
                              endAdornment: (
                                <IconButton
                                  size="small"
                                  color="primary"
                                  aria-label="add to shopping cart"
                                  onClick={() => {}}
                                >
                                  <FileCopyIcon />
                                </IconButton>
                              ),
                            }}
                            name="code"
                            onBlur={handleBlur}
                            onChange={handleChange}
                            value={values.code}
                            error={Boolean(touched.code && errors.code)}
                            helperText={touched.code && errors.code}
                          />
                        </Grid>
                        <Grid item xl={12} lg={12} md={12} sm={12} xs={12}>
                          <TextField
                            size="small"
                            fullWidth
                            label={
                              <label>
                                Nombre <span style={{ color: "red" }}>*</span>
                              </label>
                            }
                            InputLabelProps={{
                              shrink: true,
                            }}
                            variant="outlined"
                            name="name"
                            onBlur={handleBlur}
                            onChange={handleChange}
                            value={values.name}
                            error={Boolean(touched.name && errors.name)}
                            helperText={touched.name && errors.name}
                          />
                        </Grid>
                        <Grid item xl={12} lg={12} md={12} sm={12} xs={12}>
                          <TextField
                            size="small"
                            fullWidth
                            label={<label>Descripción</label>}
                            InputLabelProps={{
                              shrink: true,
                            }}
                            variant="outlined"
                            name="description"
                            onBlur={handleBlur}
                            onChange={handleChange}
                            value={values.description}
                            error={Boolean(
                              touched.description && errors.description
                            )}
                            helperText={
                              touched.description && errors.description
                            }
                          />
                        </Grid>
                        <Grid item xl={12} lg={12} md={12} sm={12} xs={12}>
                          <TextField
                            size="small"
                            label={
                              <label>
                                Item <span style={{ color: "red" }}>*</span>
                              </label>
                            }
                            name="item"
                            error={Boolean(touched.item && errors.item)}
                            helperText={touched.item && errors.item}
                            fullWidth
                            SelectProps={{ native: true }}
                            select
                            onBlur={handleBlur}
                            onChange={(e) => {
                              handleChange(e);
                            }}
                            value={values.item}
                            variant="outlined"
                            InputLabelProps={{
                              shrink: true,
                            }}
                          >
                            <option key="-1" value="-1">
                              {"-- Seleccionar --"}
                            </option>
                            {items.map((item) => (
                              <option
                                key={item.itm_c_iid}
                                value={item.itm_c_iid}
                              >
                                {item.itm_c_ccodigo}
                              </option>
                            ))}
                          </TextField>
                        </Grid>
                        <Grid
                          item
                          xl={12}
                          lg={12}
                          md={12}
                          sm={12}
                          xs={12}
                          style={{ display: "flex" }}
                        >
                          <TextField
                            size="small"
                            label={
                              <label>
                                Marca <span style={{ color: "red" }}>*</span>
                              </label>
                            }
                            name="brand"
                            error={Boolean(touched.brand && errors.brand)}
                            helperText={touched.brand && errors.brand}
                            fullWidth
                            SelectProps={{ native: true }}
                            select
                            onBlur={handleBlur}
                            onChange={(e) => {
                              setFValues(e.target.value);
                              getColor({ id: e.target.value })
                                .then((res) => {
                                  setColors(res);
                                })
                                .catch((err) => {
                                  setColors([]);
                                });
                              getCategory({ id: e.target.value })
                                .then((res) => {
                                  setCategories(res);
                                })
                                .catch((err) => {
                                  setCategories([]);
                                });
                              handleChange(e);
                            }}
                            value={values.brand}
                            variant="outlined"
                            InputLabelProps={{
                              shrink: true,
                            }}
                          >
                            <option key="-1" value="-1">
                              {"-- Seleccionar --"}
                            </option>
                            {brands.map((brand) => (
                              <option
                                key={brand.marca_c_vid}
                                value={brand.marca_c_vid}
                              >
                                {brand.marca_c_vdescripcion}
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
                        <Grid
                          item
                          xl={12}
                          lg={12}
                          md={12}
                          sm={12}
                          xs={12}
                          style={{ display: "flex" }}
                        >
                          <TextField
                            size="small"
                            label={
                              <label>
                                Color de Marca{" "}
                                <span style={{ color: "red" }}>*</span>
                              </label>
                            }
                            name="color"
                            error={Boolean(touched.color && errors.color)}
                            helperText={touched.color && errors.color}
                            fullWidth
                            SelectProps={{ native: true }}
                            select
                            onBlur={handleBlur}
                            onChange={(e) => {
                              handleChange(e);
                            }}
                            value={values.color}
                            variant="outlined"
                            InputLabelProps={{
                              shrink: true,
                            }}
                          >
                            <option key="-1" value="-1">
                              {"-- Seleccionar --"}
                            </option>
                            {colors.map((color) => (
                              <option
                                key={color.marca_color_c_vid}
                                value={color.marca_color_c_vid}
                              >
                                {color.marca_color_c_vid}
                              </option>
                            ))}
                          </TextField>
                          <IconButton
                            size="small"
                            color="secondary"
                            aria-label="add to shopping cart"
                            onClick={() => {
                              setModalState(1);
                              handleModalOpen3();
                            }}
                          >
                            <AddIcon2 />
                          </IconButton>
                        </Grid>
                      </Grid>
                    </Grid>
                    <Grid item xl={6} lg={6} md={6} sm={12} xs={12}>
                      <Grid container spacing={3}>
                        <Grid item xl={12} lg={12} md={12} sm={12} xs={12}>
                          <div style={{ padding: "10%" }}>
                            <div
                              style={{
                                minHeight: "50px",
                                position: "relative",
                                border: Boolean(touched.image && errors.image)
                                  ? "1px solid red"
                                  : null,
                              }}
                            >
                              <input
                                type="file"
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
                                  setFieldValue(
                                    "imageUrl",
                                    URL.createObjectURL(
                                      e.currentTarget.files[0]
                                    )
                                  );
                                  setFieldValue(
                                    "image",
                                    e.currentTarget.files[0]
                                  );
                                  setFieldValue("imageChange", true);
                                }}
                              />
                              {/* <Field /> */}
                              <img
                                src={
                                  values.imageUrl === null
                                    ? "/uploads/thumb.png"
                                    : values.imageUrl
                                }
                                style={{ width: "100%", maxHeight: "100%" }}
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
                        <Grid
                          item
                          xl={12}
                          lg={12}
                          md={12}
                          sm={12}
                          xs={12}
                          style={{ display: "flex" }}
                        >
                          <TextField
                            size="small"
                            label={
                              <label>
                                Categoría de Marca{" "}
                                <span style={{ color: "red" }}>*</span>
                              </label>
                            }
                            name="category"
                            error={Boolean(touched.category && errors.category)}
                            helperText={touched.category && errors.category}
                            fullWidth
                            SelectProps={{ native: true }}
                            select
                            onBlur={handleBlur}
                            onChange={(e) => {
                              handleChange(e);
                            }}
                            value={values.category}
                            variant="outlined"
                            InputLabelProps={{
                              shrink: true,
                            }}
                          >
                            <option key="-1" value="-1">
                              {"-- Seleccionar --"}
                            </option>
                            {categories.map((category) => (
                              <option
                                key={category.marca_categoria_c_vid}
                                value={category.marca_categoria_c_vid}
                              >
                                {category.marca_categoria_c_vid}
                              </option>
                            ))}
                          </TextField>
                          <IconButton
                            size="small"
                            color="secondary"
                            aria-label="add to shopping cart"
                            onClick={() => {
                              setModalState(2);
                              handleModalOpen3();
                            }}
                          >
                            <AddIcon2 />
                          </IconButton>
                        </Grid>
                        <Grid item xl={12} lg={12} md={12} sm={12} xs={12}>
                          <Grid
                            container
                            spacing={1}
                            style={{ background: "#efefef" }}
                          >
                            <Grid item xl={12} xs={12}>
                              <label>
                                &nbsp;&nbsp;&nbsp;&nbsp;Talla{" "}
                                <span style={{ color: "red" }}>*</span>
                              </label>
                            </Grid>
                            <Grid
                              item
                              xl={12}
                              xs={12}
                              style={{ maxHeight: 200, overflowY: "scroll" }}
                            >
                              {values.sizes && values.sizes.length > 0 ? (
                                <FormGroup>
                                  {values.sizes.map((t, i) => (
                                    <FormControlLabel
                                      key={i.toString()}
                                      control={
                                        <Checkbox
                                          checked={values.sizes[i].check}
                                          onChange={handleChange}
                                          name={`sizes[${i}].check`}
                                        />
                                      }
                                      label={t.key}
                                    />
                                  ))}
                                </FormGroup>
                              ) : (
                                <></>
                              )}
                            </Grid>
                          </Grid>
                        </Grid>
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
            maxWidth="xs"
            fullWidth
            onClose={handleModalClose3}
            open={isModalOpen3}
          >
            {isModalOpen3 && (
              <NewCategory
                modalState={modalState}
                brands={brands}
                originColors={originColors}
                originCategories={originCategories}
                setDefaultData={setDefaultData}
                onCancel={handleModalClose3}
              />
            )}
          </Dialog>
        </>
      )}
    </>
  );
};

NewEstilo.propTypes = {};

export default NewEstilo;
