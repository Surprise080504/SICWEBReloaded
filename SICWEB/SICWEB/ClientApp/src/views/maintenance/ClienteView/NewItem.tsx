import { useEffect, useState } from "react";
import type { FC } from "react";
import PropTypes from "prop-types";
import PerfectScrollbar from "react-perfect-scrollbar";
// eslint-disable-next-line @typescript-eslint/no-unused-vars
import _ from "lodash";
import * as Yup from "yup";
import { FieldArray, Form, Formik } from "formik";
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
  Table,
  TablePagination,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
  Tooltip,
  SvgIcon,
  FormControlLabel,
  Checkbox,
} from "@material-ui/core";
import AddIcon2 from "@material-ui/icons/Add";
import type { Theme } from "src/theme";
import { Event } from "src/types/calendar";
import {
  saveClient,
  getCargo,
  getDepartment,
  getProvince,
  getDistrict,
} from "src/apis/clienteApi";
import { useSnackbar } from "notistack";
import useSettings from "src/hooks/useSettings";
import { KeyboardDatePicker } from "@material-ui/pickers";

import { Edit as EditIcon } from "react-feather";
import DeleteIcon from "@material-ui/icons/Delete";
import NewContact from "./NewContact";
interface NewItemProps {
  editID: number;
  _initialValue?: any;
  segments?: any[];
  products?: any[];
  families?: any[];
  subFamilies?: any[];
  units?: any[];
  event?: Event;
  _getInitialData?: () => void;
  onAddComplete?: () => void;
  onCancel?: () => void;
  onDeleteComplete?: () => void;
  onEditComplete?: () => void;
}

const applyPagination = (
  clientes: any[],
  page: number,
  limit: number
): any[] => {
  return clientes.slice(page * limit, page * limit + limit);
};
const useStyles = makeStyles((theme: Theme) => ({
  root: {},
  confirmButton: {
    marginLeft: theme.spacing(2),
  },
  shippableField: {
    marginLeft: theme.spacing(2),
  },
}));

const NewItem: FC<NewItemProps> = ({
  editID,
  _initialValue,
  segments,
  products,
  families,
  units,
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

  const [modalState, setModalState] = useState(0);
  const [page, setPage] = useState<number>(0);
  const [limit] = useState<number>(15);
  const [cargo, setCargo] = useState<any>([]);
  const [department, setDepartment] = useState<any>([]);
  const [province, setProvince] = useState<any>([]);
  const [district, setDistrict] = useState<any>([]);
  const [kind, setKind] = useState<any>([]);

  const [contacts, setContacts] = useState<any>([]);
  const paginatedContact = applyPagination(contacts, page, limit);

  useEffect(() => {
    getCargo()
      .then((res) => {
        setCargo(res);
      })
      .catch((err) => {
        setCargo([]);
      });
    getDepartmentHandle();
  }, []);

  const _getCargo: any = (_c) => {
    let _cargo = "";
    for (let i = 0; i < cargo.length; i++)
      if (cargo[i].cli_contac_cargo_c_yid.toString() === _c.toString())
        _cargo = cargo[i].cli_contac_cargo_c_vnomb;
    return _cargo;
  };

  const getDepartmentHandle = () => {
    getDepartment()
      .then((res) => {
        setDepartment(res);
      })
      .catch((err) => {
        setDepartment([]);
      });
  };

  const getProvinceHandle = (d) => {
    getProvince(d)
      .then((res) => {
        setProvince(res);
      })
      .catch((err) => {
        setProvince([]);
      });
  };

  const getDistrictHandle = (p) => {
    getDistrict(p)
      .then((res) => {
        setDistrict(res);
      })
      .catch((err) => {
        setDistrict([]);
      });
  };

  const addContact = (contact: any) => {
    const _c = contacts;
    _c.push(contact);
    setContacts([..._c]);
  };

  useEffect(() => {}, [contacts]);

  const handleDelete = (id) => {
    //setDeleteID(id);
    //setIsModalOpen2(true);
  };
  const handleEdit = (id) => {
    // setEditID(id);
    // setIsModalOpen(true);
  };
  const handlePageChange = (event: any, newPage: number): void => {
    setPage(newPage);
  };

  const handleModalClose3 = (): void => {
    setIsModalOpen3(false);
  };

  const handleModalOpen3 = (): void => {
    setIsModalOpen3(true);
  };

  const getInitialValues = () => {
    return {
      id: -1,
      person: 0,
      company: "",
      ruc: "",
      // trade: [{id: 0, value: '54644364'}, {id: 1, value: 'awfsdfsdf'}, {id: 2, value: 'fefese'}, {id: 3, value: 'aefese'}],
      trade: [{ id: -1, value: "" }],
      snumber: "",
      ditem: "",
      phone: "",
      client: true,
      provider: false,
      anniversary: null, //new Date(),
      constitution: null, //new Date(),
      delivery: "1",
      direction: [
        {
          id: "-1",
          value: "",
          department: "-1",
          province: "-1",
          district: "-1",
          kind: "0",
        },
      ],
      submit: null,
    };

    // if(editID > -1) {
    //     return _.merge({}, {
    //         id: -1,
    //         person: 0,
    //         code: '',
    //         description: '',
    //         unit: -1,
    //         purchaseprice: '',
    //         saleprice: '',
    //         pid: -1,
    //         family: -1,
    //         subfamily: -1,
    //         submit: null
    //       }, {
    //             id: _initialValue[editID].itm_c_iid,
    //             person: _initialValue[editID].cli_c_btipo_pers,
    //             code: _initialValue[editID].itm_c_ccodigo,
    //             description: _initialValue[editID].itm_c_vdescripcion,
    //             unit: _initialValue[editID].und_c_yid,
    //             purchaseprice: _initialValue[editID].itm_c_dprecio_compra,
    //             saleprice: _initialValue[editID].itm_c_dprecio_venta,
    //             pid: _initialValue[editID].pro_partida_c_iid,
    //             family: _initialValue[editID].isf_c_iid,
    //             subfamily: _initialValue[editID].ifm_c_iid,
    //             submit: null
    //     });
    // }else{
    //     return {
    //         id: -1,
    //         person: 0,
    //         company: '',
    //         code: '',
    //         description: '',
    //         unit: -1,
    //         purchaseprice: '',
    //         saleprice: '',
    //         pid: -1,
    //         family: -1,
    //         subfamily: -1,
    //         submit: null
    //       };
    // }
  };

  return (
    <>
      <Formik
        initialValues={getInitialValues()}
        validationSchema={Yup.object().shape({
          company: Yup.string()
            .max(200, "Debe tener 200 caracteres como máximo")
            .required("Se requiere una razón social"),
          //ruc: Yup.string().test('len', 'El Ruc debe tener 11 dígitos', val => val.length === 11).required('Se requiere una ruc').matches(/^((\\+[1-9]{1,4}[ \\-]*)|(\\([0-9]{2,3}\\)[ \\-]*)|([0-9]{2,4})[ \\-]*)*?[0-9]{3,4}?[ \\-]*[0-9]{3,4}?$/, 'El Ruc debe tener 11 dígitos'),
          ruc: Yup.string()
            .length(11, "El Ruc debe tener 11 dígitos")
            .required("Se requiere una ruc")
            .matches(
              /^((\\+[1-9]{1,4}[ \\-]*)|(\\([0-9]{2,3}\\)[ \\-]*)|([0-9]{2,4})[ \\-]*)*?[0-9]{3,4}?[ \\-]*[0-9]{3,4}?$/,
              "El Ruc debe tener 11 dígitos"
            ),
          trade: Yup.array()
            .of(
              Yup.object().shape({
                value: Yup.string()
                  .max(200, "Debe tener 200 caracteres como máximo")
                  .required("Se requiere una Nombre Comercial"),
              })
            )
            .required("Must have Nombre Comercial")
            .min(1, "Minimum of 1 Nombre Comercial"),
          snumber: Yup.string().max(30, "Debe tener 30 caracteres como máximo"),
          ditem: Yup.string().max(200, "Debe tener 200 caracteres como máximo"),
          phone: Yup.string()
            .max(15, "Debe tener 15 caracteres como máximo")
            .matches(
              /^((\\+[1-9]{1,4}[ \\-]*)|(\\([0-9]{2,3}\\)[ \\-]*)|([0-9]{2,4})[ \\-]*)*?[0-9]{3,4}?[ \\-]*[0-9]{3,4}?$/,
              "Número de teléfono incorrecto"
            )
            .required("Must have Teléfono"),
          delivery: Yup.number().min(1).required(),
          direction: Yup.array()
            .of(
              Yup.object().shape({
                value: Yup.string()
                  .max(200, "Debe tener 200 caracteres como máximo")
                  .required("Se requiere una Nombre Comercial"),
                department: Yup.number()
                  .min(0, "Se requiere el tipo de Departamento")
                  .required("Se requiere el tipo de Departamento"),
                province: Yup.number()
                  .min(0, "Se requiere el tipo de Provincia")
                  .required("Se requiere el tipo de Provincia"),
                district: Yup.number()
                  .min(0, "Se requiere el tipo de Distrito")
                  .required("Se requiere el tipo de Distrito"),
                kind: Yup.number()
                  .min(0, "Se requiere el tipo de tipo")
                  .required("Se requiere el tipo de tipo"),
              })
            )
            .required("Must have Nombre Comercial")
            .min(1, "Minimum of 1 Nombre Comercial"),
        })}
        onSubmit={async (
          values,
          { resetForm, setErrors, setStatus, setSubmitting }
        ) => {
          saveSettings({ saving: true });
          let _v = values;
          _v["contact"] = contacts;
          window.setTimeout(() => {
            saveClient(values)
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
          <Form onSubmit={handleSubmit}>
            <Box p={3}>
              <Typography
                align="center"
                gutterBottom
                variant="h4"
                color="textPrimary"
              >
                {editID > -1
                  ? "Editar Cliente / Proveedor"
                  : "Nuevo Cliente / Proveedor"}
              </Typography>
            </Box>
            <Divider />
            <Box p={3}>
              <Grid container spacing={3}>
                <Grid item xl={4} lg={4} md={4} sm={4} xs={12}>
                  <TextField
                    size="small"
                    label={
                      <label>
                        Tipo de persona <span style={{ color: "red" }}>*</span>
                      </label>
                    }
                    name="person"
                    error={Boolean(touched.person && errors.person)}
                    helperText={
                      touched.person &&
                      errors.person &&
                      "Se requiere el tipo de persona"
                    }
                    fullWidth
                    select
                    SelectProps={{ native: true }}
                    onBlur={handleBlur}
                    onChange={(e) => {
                      // handleChange(e);
                      setFieldValue(
                        "person",
                        parseInt(e.target.value, 10) === 0
                      );
                    }}
                    value={values.person}
                    variant="outlined"
                    InputLabelProps={{
                      shrink: true,
                    }}
                    // defaultValue={0}
                  >
                    <option key={0} value={0}>
                      {" "}
                      {"NATURAL"}{" "}
                    </option>
                    <option key={1} value={1}>
                      {" "}
                      {"JURÍDICA"}{" "}
                    </option>
                    <option key={2} value={2}>
                      {" "}
                      {"EXTRANJERO"}{" "}
                    </option>
                  </TextField>
                </Grid>
                <Grid item xl={4} lg={4} md={4} sm={4} xs={12}>
                  <TextField
                    size="small"
                    error={Boolean(touched.company && errors.company)}
                    fullWidth
                    helperText={touched.company && errors.company}
                    label={
                      <label>
                        Razón Social <span style={{ color: "red" }}>*</span>
                      </label>
                    }
                    InputLabelProps={{
                      shrink: true,
                    }}
                    name="company"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    value={values.company}
                    variant="outlined"
                  />
                </Grid>
                <Grid item xl={4} lg={4} md={4} sm={4} xs={12}>
                  <TextField
                    size="small"
                    error={Boolean(touched.ruc && errors.ruc)}
                    fullWidth
                    helperText={touched.ruc && errors.ruc}
                    label={
                      <label>
                        RUC <span style={{ color: "red" }}>*</span>
                      </label>
                    }
                    InputLabelProps={{
                      shrink: true,
                    }}
                    name="ruc"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    value={values.ruc}
                    variant="outlined"
                  />
                </Grid>
              </Grid>
              <Grid container spacing={3}>
                <FieldArray
                  name="trade"
                  render={(arrayHelpers) => (
                    <Grid item xl={4} lg={4} md={4} sm={4} xs={12}>
                      <Grid container style={{ background: "#efefef" }}>
                        <Grid item xl={11} xs={11}>
                          <Grid container spacing={1}>
                            <Grid item xl={12} xs={12}>
                              <label
                                style={{
                                  fontSize: 12.5,
                                  color: "rgba(0, 0, 0, 0.54)",
                                }}
                              >
                                &nbsp;&nbsp;&nbsp;&nbsp;Nombre Comercial
                              </label>
                            </Grid>
                            {values.trade && values.trade.length > 0 ? (
                              values.trade.map((_trade, index) => (
                                <Grid key={index} item xl={12} xs={12}>
                                  <TextField
                                    size="small"
                                    error={Boolean(
                                      touched.trade &&
                                        touched.trade[index] &&
                                        touched.trade[index].value &&
                                        errors.trade &&
                                        errors.trade[index] &&
                                        errors.trade[index]
                                    )}
                                    fullWidth
                                    helperText={
                                      <>
                                        {touched.trade &&
                                          touched.trade[index] &&
                                          touched.trade[index].value &&
                                          errors.trade &&
                                          errors.trade[index] &&
                                          errors.trade[index]["value"]}
                                      </>
                                    }
                                    label={
                                      <label>
                                        <span style={{ color: "red" }}>*</span>
                                      </label>
                                    }
                                    InputLabelProps={{
                                      shrink: true,
                                    }}
                                    InputProps={{
                                      endAdornment: (
                                        <IconButton
                                          size="small"
                                          color="primary"
                                          aria-label="add to shopping cart"
                                          onClick={() => {
                                            values.trade.length > 1 &&
                                              arrayHelpers.remove(index);
                                          }}
                                        >
                                          <DeleteIcon />
                                        </IconButton>
                                      ),
                                    }}
                                    name={`trade[${index}].value`}
                                    onBlur={handleBlur}
                                    onChange={handleChange}
                                    value={values.trade[index].value}
                                    variant="outlined"
                                  />
                                </Grid>
                              ))
                            ) : (
                              <></>
                            )}
                          </Grid>
                        </Grid>
                        <Grid item xl={1} xs={1}>
                          <IconButton
                            size="small"
                            color="secondary"
                            aria-label="add to shopping cart"
                            onClick={() => {
                              arrayHelpers.push({ id: -1, value: "" });
                            }}
                          >
                            <AddIcon2 />
                          </IconButton>
                        </Grid>
                      </Grid>
                    </Grid>
                  )}
                />
                <Grid item xl={8} lg={8} md={8} sm={8} xs={12}>
                  <Grid container spacing={3}>
                    <Grid item xl={6} lg={6} md={6} sm={6} xs={12}>
                      <TextField
                        size="small"
                        error={Boolean(touched.snumber && errors.snumber)}
                        fullWidth
                        helperText={touched.snumber && errors.snumber}
                        label={<label>N° de partida</label>}
                        InputLabelProps={{
                          shrink: true,
                        }}
                        name="snumber"
                        onBlur={handleBlur}
                        onChange={handleChange}
                        value={values.snumber}
                        variant="outlined"
                      />
                    </Grid>
                    <Grid item xl={6} lg={6} md={6} sm={6} xs={12}>
                      <TextField
                        size="small"
                        error={Boolean(touched.ditem && errors.ditem)}
                        fullWidth
                        helperText={touched.ditem && errors.ditem}
                        label={<label>Rubro detallado (SUNAT)</label>}
                        InputLabelProps={{
                          shrink: true,
                        }}
                        name="ditem"
                        onBlur={handleBlur}
                        onChange={handleChange}
                        value={values.ditem}
                        variant="outlined"
                      />
                    </Grid>
                    <Grid item xl={6} lg={6} md={6} sm={6} xs={12}>
                      <TextField
                        size="small"
                        error={Boolean(touched.phone && errors.phone)}
                        fullWidth
                        helperText={touched.phone && errors.phone}
                        label={
                          <label>
                            Teléfono <span style={{ color: "red" }}>*</span>
                          </label>
                        }
                        InputLabelProps={{
                          shrink: true,
                        }}
                        name="phone"
                        onBlur={handleBlur}
                        onChange={handleChange}
                        value={values.phone}
                        variant="outlined"
                      />
                    </Grid>
                    <Grid item xl={6} lg={6} md={6} sm={6} xs={12}>
                      <FormControlLabel
                        className={classes.shippableField}
                        control={
                          <Checkbox
                            checked={values.client}
                            onChange={handleChange}
                            name="client"
                          />
                        }
                        label="Cliente"
                      />
                      <FormControlLabel
                        className={classes.shippableField}
                        control={
                          <Checkbox
                            checked={values.provider}
                            onChange={handleChange}
                            name="provider"
                          />
                        }
                        label="Proveedor"
                      />
                    </Grid>
                  </Grid>
                </Grid>
              </Grid>

              <Grid container spacing={3}>
                <Grid item xl={4} lg={4} md={4} sm={4} xs={12}>
                  <KeyboardDatePicker
                    size="small"
                    fullWidth
                    format="DD/MM/yyyy"
                    inputVariant="outlined"
                    label={<label>Fecha de Aniversario</label>}
                    name="anniversary"
                    onClick={() => setFieldTouched("anniversary")}
                    onChange={(date) => setFieldValue("anniversary", date)}
                    value={values.anniversary}
                    KeyboardButtonProps={{
                      "aria-label": "change date",
                    }}
                    cancelLabel={"Cancelar"}
                    okLabel={"OK"}
                    InputLabelProps={{
                      shrink: true,
                    }}
                  />
                </Grid>
                <Grid item xl={4} lg={4} md={4} sm={4} xs={12}>
                  <KeyboardDatePicker
                    size="small"
                    fullWidth
                    format="DD/MM/yyyy"
                    inputVariant="outlined"
                    label={<label>Fecha de Constitución</label>}
                    name="constitution"
                    onClick={() => setFieldTouched("constitution")}
                    onChange={(date) => setFieldValue("constitution", date)}
                    value={values.constitution}
                    KeyboardButtonProps={{
                      "aria-label": "change date",
                    }}
                    cancelLabel={"Cancelar"}
                    okLabel={"OK"}
                    InputLabelProps={{
                      shrink: true,
                    }}
                  />
                </Grid>
                <Grid item xl={4} lg={4} md={4} sm={4} xs={12}>
                  <TextField
                    size="small"
                    label={<label> Zona de reparto (Lima)</label>}
                    name="delivery"
                    error={Boolean(touched.delivery && errors.delivery)}
                    helperText={
                      touched.delivery &&
                      errors.delivery &&
                      "Se requiere el 	Zona de reparto"
                    }
                    fullWidth
                    SelectProps={{ native: true }}
                    select
                    onBlur={handleBlur}
                    onChange={(e) => {
                      handleChange(e);
                    }}
                    value={values.delivery}
                    variant="outlined"
                    InputLabelProps={{
                      shrink: true,
                    }}
                  >
                    {/* <option selected key="-1" value="-1">{'-- Seleccionar --'}</option> */}
                    <option key="1" value="1">
                      {"A"}
                    </option>
                    <option key="2" value="2">
                      {"B"}
                    </option>
                    <option key="3" value="3">
                      {"C"}
                    </option>
                    {/* {products.map((product) => (
                                            <option
                                            key={product.pro_partida_c_iid}
                                            value={product.pro_partida_c_iid}
                                            >
                                            {product.pro_partida_c_vdescripcion}
                                            </option>
                                        ))} */}
                  </TextField>
                </Grid>
              </Grid>

              <br />

              <Grid container spacing={0} style={{ background: "#efefef" }}>
                <FieldArray
                  name="direction"
                  render={(arrayHelpers) => (
                    <>
                      <Grid item xl={11} lg={11} sm={11} xs={11}>
                        <Grid container spacing={3}>
                          <Grid item xl={12} xs={12}>
                            <label
                              style={{
                                fontSize: 12.5,
                                color: "rgba(0, 0, 0, 0.54)",
                              }}
                            >
                              &nbsp;&nbsp;&nbsp;&nbsp;Dirección
                            </label>
                          </Grid>
                          {values.direction && values.direction.length > 0 ? (
                            values.direction.map((_direction, index) => (
                              <>
                                <Grid
                                  item
                                  xl={12}
                                  lg={12}
                                  md={12}
                                  sm={12}
                                  xs={12}
                                >
                                  <TextField
                                    size="small"
                                    error={Boolean(
                                      touched.direction &&
                                        touched.direction[index] &&
                                        touched.direction[index].value &&
                                        errors.direction &&
                                        errors.direction[index] &&
                                        errors.direction[index]["value"]
                                    )}
                                    fullWidth
                                    helperText={
                                      <>
                                        {touched.direction &&
                                          touched.direction[index] &&
                                          touched.direction[index].value &&
                                          errors.direction &&
                                          errors.direction[index] &&
                                          errors.direction[index]["value"]}
                                      </>
                                    }
                                    label={
                                      <label>
                                        <span style={{ color: "red" }}>*</span>
                                      </label>
                                    }
                                    InputLabelProps={{
                                      shrink: true,
                                    }}
                                    InputProps={{
                                      endAdornment: (
                                        <IconButton
                                          size="small"
                                          color="primary"
                                          aria-label="add to shopping cart"
                                          onClick={() => {
                                            values.direction.length > 1 &&
                                              arrayHelpers.remove(index);
                                          }}
                                        >
                                          <DeleteIcon />
                                        </IconButton>
                                      ),
                                    }}
                                    name={`direction[${index}].value`}
                                    onBlur={handleBlur}
                                    onChange={handleChange}
                                    value={values.direction[index].value}
                                    variant="outlined"
                                  />
                                </Grid>
                                <Grid item xl={3} lg={3} md={3} sm={3} xs={12}>
                                  <TextField
                                    size="small"
                                    label={
                                      <label>
                                        Departamento{" "}
                                        <span style={{ color: "red" }}>*</span>
                                      </label>
                                    }
                                    name={`direction[${index}].department`}
                                    error={Boolean(
                                      touched.direction &&
                                        touched.direction[index] &&
                                        touched.direction[index].department &&
                                        errors.direction &&
                                        errors.direction[index] &&
                                        errors.direction[index]["department"]
                                    )}
                                    helperText={
                                      <>
                                        {touched.direction &&
                                          touched.direction[index] &&
                                          touched.direction[index].department &&
                                          errors.direction &&
                                          errors.direction[index] &&
                                          errors.direction[index]["department"]}
                                      </>
                                    }
                                    fullWidth
                                    SelectProps={{ native: true }}
                                    select
                                    onBlur={handleBlur}
                                    onChange={(e) => {
                                      getProvinceHandle(e.target.value);
                                      handleChange(e);
                                    }}
                                    value={values.direction[index].department}
                                    variant="outlined"
                                    InputLabelProps={{
                                      shrink: true,
                                    }}
                                  >
                                    <option
                                      defaultValue="-1"
                                      key="-1"
                                      value="-1"
                                    >
                                      {"-- Seleccionar --"}
                                    </option>
                                    {department.map((d) => (
                                      <option
                                        key={d.depa_c_ccod}
                                        value={d.depa_c_ccod}
                                      >
                                        {d.depa_c_vnomb}
                                      </option>
                                    ))}
                                  </TextField>
                                </Grid>
                                <Grid item xl={3} lg={3} md={3} sm={3} xs={12}>
                                  <TextField
                                    size="small"
                                    label={
                                      <label>
                                        Provincia{" "}
                                        <span style={{ color: "red" }}>*</span>
                                      </label>
                                    }
                                    name={`direction[${index}].province`}
                                    error={Boolean(
                                      touched.direction &&
                                        touched.direction[index] &&
                                        touched.direction[index].province &&
                                        errors.direction &&
                                        errors.direction[index] &&
                                        errors.direction[index]["province"]
                                    )}
                                    helperText={
                                      <>
                                        {touched.direction &&
                                          touched.direction[index] &&
                                          touched.direction[index].province &&
                                          errors.direction &&
                                          errors.direction[index] &&
                                          errors.direction[index]["province"]}
                                      </>
                                    }
                                    fullWidth
                                    SelectProps={{ native: true }}
                                    select
                                    onBlur={handleBlur}
                                    onChange={(e) => {
                                      getDistrictHandle(e.target.value);
                                      handleChange(e);
                                    }}
                                    value={values.direction[index].province}
                                    variant="outlined"
                                    InputLabelProps={{
                                      shrink: true,
                                    }}
                                  >
                                    <option
                                      defaultValue="-1"
                                      key="-1"
                                      value="-1"
                                    >
                                      {"-- Seleccionar --"}
                                    </option>
                                    {province.map((p) => (
                                      <option
                                        key={p.prov_c_ccod}
                                        value={p.prov_c_ccod}
                                      >
                                        {p.prov_c_vnomb}
                                      </option>
                                    ))}
                                  </TextField>
                                </Grid>
                                <Grid item xl={3} lg={3} md={3} sm={3} xs={12}>
                                  <TextField
                                    size="small"
                                    label={
                                      <label>
                                        Distrito{" "}
                                        <span style={{ color: "red" }}>*</span>
                                      </label>
                                    }
                                    name={`direction[${index}].district`}
                                    error={Boolean(
                                      touched.direction &&
                                        touched.direction[index] &&
                                        touched.direction[index].district &&
                                        errors.direction &&
                                        errors.direction[index] &&
                                        errors.direction[index]["district"]
                                    )}
                                    helperText={
                                      <>
                                        {touched.direction &&
                                          touched.direction[index] &&
                                          touched.direction[index].district &&
                                          errors.direction &&
                                          errors.direction[index] &&
                                          errors.direction[index]["district"]}
                                      </>
                                    }
                                    fullWidth
                                    SelectProps={{ native: true }}
                                    select
                                    onBlur={handleBlur}
                                    onChange={(e) => {
                                      handleChange(e);
                                    }}
                                    value={values.direction[index].district}
                                    variant="outlined"
                                    InputLabelProps={{
                                      shrink: true,
                                    }}
                                  >
                                    <option
                                      defaultValue="-1"
                                      key="-1"
                                      value="-1"
                                    >
                                      {"-- Seleccionar --"}
                                    </option>
                                    {district.map((d) => (
                                      <option
                                        key={d.dist_c_ccod_ubig}
                                        value={d.dist_c_ccod_ubig}
                                      >
                                        {d.dist_c_vnomb}
                                      </option>
                                    ))}
                                  </TextField>
                                </Grid>
                                <Grid item xl={3} lg={3} md={3} sm={3} xs={12}>
                                  <TextField
                                    size="small"
                                    label={
                                      <label>
                                        {" "}
                                        Zona de reparto (Lima){" "}
                                        <span style={{ color: "red" }}>*</span>
                                      </label>
                                    }
                                    name={`direction[${index}].kind`}
                                    error={Boolean(
                                      touched.direction &&
                                        touched.direction[index] &&
                                        touched.direction[index].kind &&
                                        errors.direction &&
                                        errors.direction[index] &&
                                        errors.direction[index]["kind"]
                                    )}
                                    helperText={
                                      <>
                                        {touched.direction &&
                                          touched.direction[index] &&
                                          touched.direction[index].kind &&
                                          errors.direction &&
                                          errors.direction[index] &&
                                          errors.direction[index]["kind"]}
                                      </>
                                    }
                                    fullWidth
                                    SelectProps={{ native: true }}
                                    select
                                    onBlur={handleBlur}
                                    onChange={(e) => {
                                      handleChange(e);
                                    }}
                                    value={values.direction[index].kind}
                                    variant="outlined"
                                    InputLabelProps={{
                                      shrink: true,
                                    }}
                                  >
                                    <option key="0" value="0">
                                      Tipo
                                    </option>
                                    <option key="1" value="1">
                                      Fiscal
                                    </option>
                                    <option key="2" value="2">
                                      Sucursal
                                    </option>
                                    <option key="3" value="3">
                                      Facturación
                                    </option>
                                    {/* <option selected key="-1" value="-1">{'-- Seleccionar --'}</option> */}
                                    {/* {products.map((product) => (
                                                                            <option
                                                                            key={product.pro_partida_c_iid}
                                                                            value={product.pro_partida_c_iid}
                                                                            >
                                                                            {product.pro_partida_c_vdescripcion}
                                                                            </option>
                                                                        ))} */}
                                  </TextField>
                                </Grid>
                              </>
                            ))
                          ) : (
                            <></>
                          )}
                        </Grid>
                      </Grid>
                      <Grid item xl={1} lg={1} sm={1} xs={1}>
                        <IconButton
                          size="small"
                          color="secondary"
                          aria-label="add to shopping cart"
                          onClick={() => {
                            arrayHelpers.push({
                              id: -1,
                              value: "",
                              department: -1,
                              province: -1,
                              district: -1,
                              kind: -1,
                            });
                          }}
                        >
                          <AddIcon2 />
                        </IconButton>
                      </Grid>
                    </>
                  )}
                />
              </Grid>
            </Box>
            <Divider />

            <Box
              p={3}
              style={{
                display: "flex",
                flexDirection: "column",
                alignItems: "flex-end",
              }}
            >
              <Button
                variant="contained"
                color="primary"
                onClick={() => handleModalOpen3()}
              >
                Agregar
              </Button>
            </Box>

            <PerfectScrollbar>
              <Box minWidth={1200}>
                <Table stickyHeader>
                  <TableHead style={{ background: "red" }}>
                    <TableRow>
                      <TableCell>N</TableCell>
                      <TableCell>NOMBRES Y APELLIDOS</TableCell>
                      <TableCell>EMAIL</TableCell>
                      <TableCell>CARGO</TableCell>
                      <TableCell align="right">&nbsp;</TableCell>
                    </TableRow>
                  </TableHead>
                  <TableBody>
                    {paginatedContact.map((item, index) => {
                      return (
                        <TableRow style={{ height: 30 }} hover key={index}>
                          <TableCell>
                            {item.id * 1 > 0 ? index + 1 : "_"}
                          </TableCell>
                          <TableCell>
                            {item.surname}
                            {item.lastname}, {item.name}
                          </TableCell>
                          <TableCell>{item.email}</TableCell>
                          <TableCell>{_getCargo(item.cargo)}</TableCell>
                          <TableCell align="right">
                            <Tooltip title="Editar" aria-label="Editar">
                              <IconButton onClick={() => handleEdit(index)}>
                                <SvgIcon fontSize="small">
                                  <EditIcon />
                                </SvgIcon>
                              </IconButton>
                            </Tooltip>
                            <Tooltip title="Eliminar" aria-label="Eliminar">
                              <IconButton onClick={() => handleDelete(item.id)}>
                                <SvgIcon fontSize="small">
                                  <DeleteIcon />
                                </SvgIcon>
                              </IconButton>
                            </Tooltip>
                          </TableCell>
                        </TableRow>
                      );
                    })}
                  </TableBody>
                </Table>
                <TablePagination
                  component="div"
                  count={contacts.length}
                  onPageChange={handlePageChange}
                  onRowsPerPageChange={() => {}}
                  page={page}
                  rowsPerPage={limit}
                  rowsPerPageOptions={[15]}
                />
              </Box>
            </PerfectScrollbar>

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
          </Form>
        )}
      </Formik>
      <Dialog
        maxWidth="sm"
        fullWidth
        onClose={handleModalClose3}
        open={isModalOpen3}
      >
        {isModalOpen3 && (
          <NewContact
            modalState={modalState}
            cargo={cargo}
            onCancel={handleModalClose3}
            addContact={(c) => addContact(c)}
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
