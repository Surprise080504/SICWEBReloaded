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
  Divider,
  FormHelperText,
  makeStyles,
  Grid,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
  Dialog,
  Tooltip,
  IconButton,
  SvgIcon,
  Checkbox,
  FormControlLabel,
  TextareaAutosize,
} from "@material-ui/core";
import { Trash as DeleteIcon } from "react-feather";
import AddIcon2 from "@material-ui/icons/Add";
import type { Theme } from "src/theme";
import { useSnackbar } from "notistack";
import useSettings from "src/hooks/useSettings";
import {
  saveOrdens,
  getClase,
  getDlvrAddr,
  getEstado,
  getDirection,
  getMoneda,
  obtNroSerie,
} from "src/apis/comprasApi";
import { useHistory } from "react-router-dom";
import ItemTables from "./ItemTable";
import ProveedorTables from "./ProveedorTables";
import AdapterDateFns from "@mui/lab/AdapterDateFns";
import LocalizationProvider from "@mui/lab/LocalizationProvider";
import DatePicker from "@mui/lab/DatePicker";
import moment from "moment";

interface NewOrdenProps {
  editID: any;
  initialValue: any;
  initialItem: any[];
  editable: boolean;
  onCancel?: () => void;
  handleSearch?: () => void;
}

const useStyles = makeStyles((theme: Theme) => ({
  root: {},
  confirmButton: {
    marginLeft: theme.spacing(2),
  },
}));

const NewOrden: FC<NewOrdenProps> = ({
  editID,
  initialValue,
  initialItem,
  editable,
  onCancel,
  handleSearch,
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

  const [ready, setReady] = useState(true);
  const [imageData, setImageData] = useState(null);

  const [claseOptions, setClaseOptions] = useState<any>([]);
  const [dirOptions, setDirOptions] = useState<any>([]);
  const [monedaOptions, setMonedaOptions] = useState<any>([]);
  const [estadoOptions, setEstadoOptions] = useState<any>([]);
  const [startDate, setStartDate] = useState<any>(null);
  const [endDate, setEndDate] = useState<any>(null);
  const [issuedate, setIssuedate] = useState<any>(null);
  const [initialOrden, setInitialOrden] = useState<any>();
  const [proveedorModalOpen, setProveedorModalOpen] = useState<any>();

  const [percentcheck, setPercentcheck] = useState<any>(false);
  const [rucValue, setRucValue] = useState<any>();
  const [proveedorValue, setProveedorValue] = useState<any>();
  const [serie, setSerie] = useState<any>();
  const [codigo, setCodigo] = useState<any>();

  const [vdescestado, setVdescestado] = useState<any>();
  const [vdescmoneda, setVdescmoneda] = useState<any>();
  const [sestado, setSestado] = useState<any>();
  const [smoneda, setSmoneda] = useState<any>();

  const getCheckedValue = (res) => {
    let tmp = items;
    res.map((item) => {
      if (item.checkstate) {
        let _subItems = tmp.filter((i: any) => i?.itm_c_iid === item.itm_c_iid);
        if (_subItems?.length == 0) {
          tmp.push(item);
        }
      }
    });

    setItems(tmp);
  };

  const getProveedorCheckedValue = (res) => {
    setProveedorValue(res.social);
    setRucValue(res.ruc);
  };

  const history = useHistory();

  const handleModalOpen3 = (): void => {
    setIsModalOpen3(true);
  };

  const handleModalClose3 = (): void => {
    setIsModalOpen3(false);
  };

  const handleProveedorModalOpen = (): void => {
    setProveedorModalOpen(true);
  };

  const handleProveedorModalClose = (): void => {
    setProveedorModalOpen(false);
  };

  const getTotal = () => {
    var sum = 0;
    items.map((item) => {
      sum += item.quantity * item.und_c_yid;
    });
    return sum;
  };

  const getSubTotal = (item) => {
    var sum = 0;
    sum = item.quantity * item.und_c_yid;
    return sum;
  };

  const _getEstado = () => {
    getEstado().then((res) => {
      setEstadoOptions(res);
    });
  };

  const _getMoneda = () => {
    getMoneda().then((res) => {
      setMonedaOptions(res);
    });
  };

  const _getClase = () => {
    getClase().then((res) => {
      setClaseOptions(res);
    });
  };

  const handlePercentCheck = (e) => {
    setPercentcheck(e.target.checked);
  };

  const handleOptChange = (e) => {
    let index = e.target.selectedIndex;
    if (e.target.name == "estado") {
      setSestado(e.target.value);
      setVdescestado(e.target[index].text);
    } else if (e.target.name == "moneda") {
      setSmoneda(e.target.value);
      setVdescmoneda(e.target[index].text);
    }
  };

  const _getDirection = () => {
    getDirection().then((res) => {
      setDirOptions(res);
    });
  };

  const handleDelete = (e, id) => {
    e.preventDefault();
    let temp = [...items];
    items.map((item) => {
      if (item.itm_c_iid == id) {
        temp.splice(items.indexOf(item), 1);
      }
    });
    setItems(temp);
  };

  const _getSerie = () => {
    obtNroSerie().then((res) => {
      setSerie(res);
    });
  };

  useEffect(() => {
    _getClase();
    _getEstado();
    _getDirection();
    _getMoneda();

    if (Number(editID) > -1) {
      setSerie(initialValue?.odc_c_cserie);
    } else {
      _getSerie();
    }

    setPercentcheck(initialValue?.odc_c_bpercepcion);
    setStartDate(initialValue?.odc_c_zfechaentrega_ini);
    setEndDate(initialValue?.odc_c_zfechaentrega_fin);
    setIssuedate(initialValue?.odc_c_zfechaemi);
    setRucValue(initialValue?.prov_c_vdoc_id);
    setProveedorValue(initialValue?.proveedor);
    setCodigo(initialValue?.odc_c_vcodigo);
    setVdescestado(initialValue?.odc_c_vdescestado);
    setVdescmoneda(initialValue?.odc_c_vdescmoneda);
    setSestado(initialValue?.odc_c_iestado);
    setSmoneda(initialValue?.odc_c_ymoneda);

    setItems(initialItem);
  }, []);

  const getInitialValues = () => {
    if (Number(editID) > -1) {
      return _.merge(
        {},
        {
          id: -1,
          serie: "",
          codigo: "",
          ruc_proveedor: "",
          proveedor: "",
          entregastart: "",
          entregaend: "",
          clase: 1,
          dlvaddr: 1,
          issuedate: "",
          estado: 1,
          moneda: 0,
          vdescestado: "",
          vdescmoneda: "",
          items: [],
          observe: "",
          percentcheck: false,
          total: 0,
          subtotal: 0,
          igvcal: 0,
          perceptioncal: 0,
          submit: null,
        },
        {
          id: initialValue?.odc_c_iid,
          serie: initialValue?.odc_c_cserie,
          codigo: initialValue?.odc_c_vcodigo,
          ruc_proveedor: initialValue?.prov_c_vdoc_id,
          proveedor: initialValue?.proveedor,
          entregastart: initialValue?.odc_c_zfechaentrega_ini,
          entregaend: initialValue?.odc_c_zfechaentrega_fin,
          clase: initialValue?.odc_c_clase_iid,
          dlvaddr: initialValue?.emp_dir_c_iid,
          issuedate: initialValue?.odc_c_zfechaemi,
          estado: initialValue?.odc_c_iestado,
          moneda: initialValue?.odc_c_ymoneda,
          vdescestado: initialValue?.odc_c_vdescestado,
          vdescmoneda: initialValue?.odc_c_vdescmoneda,
          items: items,
          observe: initialValue?.odc_c_vobservacion,
          percentcheck: initialValue?.odc_c_bpercepcion,
          total: initialValue?.odc_c_etotal,
          subtotal: initialValue?.odc_c_esubtotal,
          igvcal: initialValue?.odc_c_eigvcal,
          perceptioncal: initialValue?.odc_c_epercepcioncal,
          submit: null,
        }
      );
    } else {
      return {
        id: -1,
        serie: "",
        codigo: "",
        ruc_proveedor: "",
        proveedor: "",
        entregastart: "",
        entregaend: "",
        clase: 1,
        dlvaddr: 1,
        issuedate: "",
        estado: 1,
        moneda: 0,
        vdescestado: "",
        vdescmoneda: "",
        items: [],
        observe: "",
        percentcheck: false,
        total: 0,
        subtotal: 0,
        igvcal: 0,
        perceptioncal: 0,
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
            onSubmit={async (
              values,
              { resetForm, setErrors, setStatus, setSubmitting }
            ) => {
              saveSettings({ saving: true });
              window.setTimeout(() => {
                values.issuedate = moment(issuedate);
                //.format("MM-DD-YYYY");
                values.entregastart = moment(startDate);
                //                      .format("YYYY-MM-DD");
                values.entregaend = moment(endDate);
                //                      .format("YYYY-MM-DD");
                values.clase = Number(values.clase);
                values.dlvaddr = Number(values.dlvaddr);
                values.estado = Number(sestado);
                values.moneda = Number(smoneda);
                values.vdescestado = vdescestado;
                values.vdescmoneda = vdescmoneda;
                values.items = items;
                values.total = percentcheck
                  ? Math.floor(getTotal() * 1.2 * 100) / 100
                  : Math.floor(getTotal() * 1.18 * 100) / 100;
                values.percentcheck = percentcheck;
                values.ruc_proveedor = rucValue;
                values.serie = serie;
                console.log(values, "Aaa");
                saveOrdens(values)
                  .then((res) => {
                    saveSettings({ saving: false });
                    resetForm();
                    setStatus({ success: true });
                    setSubmitting(false);

                    if (res == "-1") {
                      enqueueSnackbar("No se pudo guardar.", {
                        variant: "error",
                      });
                    } else {
                      enqueueSnackbar(
                        "Tus datos se han guardado exitosamente.",
                        {
                          variant: "success",
                        }
                      );
                      onCancel();
                    }
                  })
                  .catch((err) => {
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
                <Box p={1}>
                  <Typography
                    align="center"
                    gutterBottom
                    variant="h4"
                    color="textPrimary"
                  >
                    {editID > -1 ? "Editar ORDEN" : "Nuevo ORDEN"}
                  </Typography>
                </Box>
                <Divider />
                <Box p={3}>
                  <Grid container spacing={3}>
                    <Grid item xl={8} lg={8} md={8} sm={12} xs={12}>
                      <Grid container spacing={3}>
                        <Grid
                          item
                          xl={3}
                          lg={3}
                          md={3}
                          sm={12}
                          xs={12}
                          style={{ display: "flex", alignItems: "center" }}
                        >
                          <label>Código: </label>
                        </Grid>
                        <Grid item xl={2} lg={2} md={2} sm={12} xs={12}>
                          <TextField
                            size="small"
                            fullWidth
                            label={<label>SERIE</label>}
                            InputLabelProps={{
                              shrink: true,
                            }}
                            disabled={true}
                            variant="outlined"
                            name="serie"
                            value={serie}
                            error={Boolean(touched.serie && errors.serie)}
                            helperText={touched.serie && errors.serie}
                          />
                        </Grid>
                        <Grid
                          item
                          xl={1}
                          lg={1}
                          md={1}
                          sm={12}
                          xs={12}
                          style={{ textAlign: "center" }}
                        >
                          <label>-</label>
                        </Grid>
                        <Grid item xl={3} lg={3} md={3} sm={12} xs={12}>
                          <TextField
                            size="small"
                            fullWidth
                            label={<label>CÓDIGO</label>}
                            InputLabelProps={{
                              shrink: true,
                            }}
                            variant="outlined"
                            name="codigo"
                            disabled={true}
                            value={codigo}
                            error={Boolean(touched.codigo && errors.codigo)}
                            helperText={touched.codigo && errors.codigo}
                          />
                        </Grid>
                      </Grid>
                      <Grid container spacing={3}>
                        <Grid
                          item
                          xl={3}
                          lg={3}
                          md={3}
                          sm={12}
                          xs={12}
                          style={{ display: "flex", alignItems: "center" }}
                        >
                          <label>Proveedor: </label>
                        </Grid>
                        <Grid item xl={2} lg={2} md={2} sm={12} xs={12}>
                          <TextField
                            size="small"
                            fullWidth
                            label={<label>RUC PROVEEDOR</label>}
                            InputLabelProps={{
                              shrink: true,
                            }}
                            variant="outlined"
                            name="ruc_proveedor"
                            value={rucValue}
                            disabled={true}
                            error={Boolean(
                              touched.ruc_proveedor && errors.ruc_proveedor
                            )}
                            helperText={
                              touched.ruc_proveedor && errors.ruc_proveedor
                            }
                          />
                        </Grid>
                        <Grid
                          item
                          xl={1}
                          lg={1}
                          md={1}
                          sm={12}
                          xs={12}
                          style={{ textAlign: "center" }}
                        >
                          <label>-</label>
                        </Grid>
                        <Grid item xl={4} lg={4} md={4} sm={12} xs={12}>
                          <TextField
                            size="small"
                            fullWidth
                            label={<label>PROVEEDOR</label>}
                            InputLabelProps={{
                              shrink: true,
                            }}
                            variant="outlined"
                            name="proveedor"
                            disabled={true}
                            value={proveedorValue}
                            error={Boolean(
                              touched.proveedor && errors.proveedor
                            )}
                            helperText={touched.proveedor && errors.proveedor}
                          />
                        </Grid>
                        <Grid item xl={2} lg={2} md={2} sm={12} xs={12}>
                          <Button
                            onClick={() => {
                              handleProveedorModalOpen();
                            }}
                            variant="contained"
                            color="primary"
                            disabled={editable}
                            startIcon={<AddIcon2 />}
                          >
                            {"Buscar"}
                          </Button>
                        </Grid>
                      </Grid>
                      <Grid container spacing={3}>
                        <Grid
                          item
                          xl={3}
                          lg={3}
                          md={3}
                          sm={12}
                          xs={12}
                          style={{ display: "flex", alignItems: "center" }}
                        >
                          <label>Rango de entrega: </label>
                        </Grid>
                        <Grid item xl={3} lg={3} md={3} sm={12} xs={12}>
                          <LocalizationProvider dateAdapter={AdapterDateFns}>
                            <DatePicker
                              label="COMIENZO"
                              value={startDate}
                              inputFormat="dd/MM/yyyy"
                              onChange={(newValue) => {
                                setStartDate(newValue);
                              }}
                              renderInput={(params: any) => (
                                <TextField {...params} />
                              )}
                            />
                          </LocalizationProvider>
                        </Grid>
                        <Grid
                          item
                          xl={1}
                          lg={1}
                          md={1}
                          sm={12}
                          xs={12}
                          style={{ display: "flex", alignItems: "center" }}
                        >
                          <label>-</label>
                        </Grid>
                        <Grid item xl={5} lg={5} md={5} sm={12} xs={12}>
                          <LocalizationProvider dateAdapter={AdapterDateFns}>
                            <DatePicker
                              label="FIN"
                              value={endDate}
                              inputFormat="dd/MM/yyyy"
                              onChange={(newValue) => {
                                setEndDate(newValue);
                              }}
                              renderInput={(params: any) => (
                                <TextField {...params} />
                              )}
                            />
                          </LocalizationProvider>
                        </Grid>
                      </Grid>
                      <Grid container spacing={3}>
                        <Grid
                          item
                          xl={3}
                          lg={3}
                          md={3}
                          sm={12}
                          xs={12}
                          style={{ display: "flex", alignItems: "center" }}
                        >
                          <label>Clase: </label>
                        </Grid>
                        <Grid item xl={9} lg={9} md={9} sm={12} xs={12}>
                          <TextField
                            fullWidth
                            size="small"
                            placeholder=""
                            variant="outlined"
                            value={values.clase}
                            name="clase"
                            SelectProps={{ native: true }}
                            select
                            onChange={handleChange}
                          >
                            {claseOptions.map((option) => (
                              <option
                                key={option.odc_cla_iid}
                                value={option.odc_cla_iid}
                              >
                                {option.odc_cla_vdes}
                              </option>
                            ))}
                          </TextField>
                        </Grid>
                      </Grid>
                      <Grid container spacing={3}>
                        <Grid
                          item
                          xl={3}
                          lg={3}
                          md={3}
                          sm={12}
                          xs={12}
                          style={{ display: "flex", alignItems: "center" }}
                        >
                          <label>Direccion de entrega: </label>
                        </Grid>
                        <Grid item xl={9} lg={9} md={9} sm={12} xs={12}>
                          <TextField
                            fullWidth
                            size="small"
                            placeholder=""
                            variant="outlined"
                            value={values.dlvaddr}
                            name="dlvaddr"
                            SelectProps={{ native: true }}
                            select
                            onChange={handleChange}
                          >
                            {dirOptions.map((option) => (
                              <option key={option.id} value={option.id}>
                                {option.label}
                              </option>
                            ))}
                          </TextField>
                        </Grid>
                      </Grid>
                    </Grid>
                    <Grid item xl={4} lg={4} md={4} sm={12} xs={12}>
                      <Grid container spacing={3}>
                        <Grid
                          item
                          xl={4}
                          lg={4}
                          md={4}
                          sm={12}
                          xs={12}
                          style={{ display: "flex", alignItems: "center" }}
                        >
                          <label>Fecha Emsión: </label>
                        </Grid>
                        <Grid item xl={8} lg={8} md={8} sm={12} xs={12}>
                          <LocalizationProvider dateAdapter={AdapterDateFns}>
                            <DatePicker
                              label="Fecha Emsión"
                              value={issuedate}
                              inputFormat="dd/MM/yyyy"
                              onChange={(newValue) => {
                                setIssuedate(newValue);
                              }}
                              renderInput={(params: any) => (
                                <TextField {...params} />
                              )}
                            />
                          </LocalizationProvider>
                        </Grid>
                      </Grid>
                      <Grid container spacing={3}>
                        <Grid
                          item
                          xl={4}
                          lg={4}
                          md={4}
                          sm={12}
                          xs={12}
                          style={{ display: "flex", alignItems: "center" }}
                        >
                          <label>Estado: </label>
                        </Grid>
                        <Grid item xl={8} lg={8} md={8} sm={12} xs={12}>
                          <TextField
                            fullWidth
                            size="small"
                            placeholder=""
                            variant="outlined"
                            value={sestado}
                            name="estado"
                            SelectProps={{ native: true }}
                            select
                            onChange={(e) => {
                              handleOptChange(e);
                            }}
                          >
                            <option key="-1" value="-1">
                              {"-- Seleccionar --"}
                            </option>
                            {estadoOptions.map((option) => (
                              <option
                                key={option.odc_estado_iid}
                                value={option.odc_estado_iid}
                              >
                                {option.odc_estado_vdescripcion}
                              </option>
                            ))}
                          </TextField>
                        </Grid>
                      </Grid>
                      <Grid container spacing={3}>
                        <Grid
                          item
                          xl={4}
                          lg={4}
                          md={4}
                          sm={12}
                          xs={12}
                          style={{ display: "flex", alignItems: "center" }}
                        >
                          <label>Moneda: </label>
                        </Grid>
                        <Grid item xl={8} lg={8} md={8} sm={12} xs={12}>
                          <TextField
                            fullWidth
                            size="small"
                            placeholder=""
                            variant="outlined"
                            value={smoneda}
                            name="moneda"
                            SelectProps={{
                              native: true,
                            }}
                            select
                            onChange={(e) => {
                              handleOptChange(e);
                            }}
                          >
                            <option key="-1" value="-1">
                              {"-- Seleccionar --"}
                            </option>
                            {monedaOptions.map((option) => (
                              <option
                                key={option.par_det_c_iid}
                                value={option.par_det_c_iid}
                              >
                                {option.par_det_c_vdesc}
                              </option>
                            ))}
                          </TextField>
                        </Grid>
                      </Grid>
                    </Grid>
                  </Grid>
                  <Grid container spacing={3}>
                    <Grid item xl={10} lg={10} md={10} sm={12} xs={12}>
                      <Table stickyHeader>
                        <TableHead style={{ background: "red" }}>
                          <TableRow>
                            <TableCell>Código</TableCell>
                            <TableCell>Descripción</TableCell>
                            <TableCell>Cantidad</TableCell>
                            <TableCell>Unitario</TableCell>
                            <TableCell>Sub Total</TableCell>
                            <TableCell></TableCell>
                            <TableCell>Unit. Ref. (Soles)</TableCell>
                          </TableRow>
                        </TableHead>
                        <TableBody>
                          {items.map((process, index) => (
                            <TableRow
                              style={{ height: 30 }}
                              hover
                              // key={item.estilo_c_iid}
                              key={index}
                            >
                              <TableCell>
                                <label>{items[index].itm_c_ccodigo}</label>
                              </TableCell>
                              <TableCell>
                                <label>{items[index].itm_c_vdescripcion}</label>
                              </TableCell>
                              <TableCell>
                                <TextField
                                  style={{
                                    width: 150,
                                  }}
                                  name={`items[${index}].quantity`}
                                  error={Boolean(
                                    touched.items &&
                                      touched.items[index] &&
                                      touched.items[index].quantity &&
                                      errors.items &&
                                      errors.items[index] &&
                                      errors.items[index]["quantity"]
                                  )}
                                  type="number"
                                  fullWidth
                                  helperText={
                                    <>
                                      {touched.items &&
                                        touched.items[index] &&
                                        touched.items[index].quantity &&
                                        errors.items &&
                                        errors.items[index] &&
                                        errors.items[index]["quantity"]}
                                    </>
                                  }
                                  onBlur={handleBlur}
                                  value={items[index].quantity}
                                  onChange={(e) => {
                                    var _temp = items;
                                    _temp[index].quantity = e.target.value;
                                    setFieldValue("items", _temp);
                                  }}
                                />
                              </TableCell>
                              <TableCell>
                                <TextField
                                  style={{
                                    width: 150,
                                  }}
                                  name={`items[${index}].und_c_yid`}
                                  error={Boolean(
                                    touched.items &&
                                      touched.items[index] &&
                                      touched.items[index].und_c_yid &&
                                      errors.items &&
                                      errors.items[index] &&
                                      errors.items[index]["und_c_yid"]
                                  )}
                                  type="number"
                                  fullWidth
                                  helperText={
                                    <>
                                      {touched.items &&
                                        touched.items[index] &&
                                        touched.items[index].und_c_yid &&
                                        errors.items &&
                                        errors.items[index] &&
                                        errors.items[index]["und_c_yid"]}
                                    </>
                                  }
                                  onBlur={handleBlur}
                                  value={items[index].und_c_yid}
                                  onChange={(e) => {
                                    var _temp = items;
                                    _temp[index].und_c_yid = e.target.value;
                                    setFieldValue("items", _temp);
                                  }}
                                />
                              </TableCell>
                              <TableCell>
                                <label>{getSubTotal(items[index])}</label>
                              </TableCell>
                              <TableCell>
                                <Tooltip title="Eliminar" aria-label="Eliminar">
                                  <IconButton
                                    onClick={(e) =>
                                      handleDelete(e, items[index].itm_c_iid)
                                    }
                                  >
                                    <SvgIcon fontSize="small">
                                      <DeleteIcon />
                                    </SvgIcon>
                                  </IconButton>
                                </Tooltip>
                              </TableCell>
                              <TableCell></TableCell>
                            </TableRow>
                          ))}
                        </TableBody>
                      </Table>
                    </Grid>
                    <Grid item xl={2} lg={2} md={2} sm={12} xs={12}>
                      <Button
                        onClick={() => {
                          handleModalOpen3();
                        }}
                        variant="contained"
                        color="primary"
                        disabled={editable}
                        startIcon={<AddIcon2 />}
                      >
                        {"Buscar Items"}
                      </Button>
                    </Grid>
                  </Grid>
                  <Grid container spacing={3}>
                    <Grid item xl={8} lg={8} md={8} sm={12} xs={12}>
                      <Grid container spacing={3}>
                        <Grid
                          item
                          xl={3}
                          lg={3}
                          md={3}
                          sm={12}
                          xs={12}
                          style={{ display: "flex", alignItems: "center" }}
                        >
                          <label>Tasa de Cambio: </label>
                        </Grid>
                        <Grid item xl={9} lg={9} md={9} sm={12} xs={12}>
                          <label>S/. 2.7970</label>
                        </Grid>
                      </Grid>
                      <Grid container spacing={3}>
                        <Grid
                          item
                          xl={3}
                          lg={3}
                          md={3}
                          sm={12}
                          xs={12}
                          style={{ display: "flex", alignItems: "center" }}
                        >
                          <label>I.G.V. (18% ): </label>
                        </Grid>
                        <Grid item xl={9} lg={9} md={9} sm={12} xs={12}>
                          <label>
                            S/. {Math.floor(getTotal() * 0.18 * 100) / 100}
                          </label>
                        </Grid>
                      </Grid>
                      <Grid container spacing={3}>
                        <Grid
                          item
                          xl={3}
                          lg={3}
                          md={3}
                          sm={12}
                          xs={12}
                          style={{ display: "flex", alignItems: "center" }}
                        >
                          <label>Sub Total: </label>
                        </Grid>
                        <Grid item xl={9} lg={9} md={9} sm={12} xs={12}>
                          <label>
                            {" "}
                            S/. {Math.floor(getTotal() * 100) / 100}
                          </label>
                        </Grid>
                      </Grid>
                      <Grid container spacing={3}>
                        <Grid
                          item
                          xl={3}
                          lg={3}
                          md={3}
                          sm={12}
                          xs={12}
                          style={{ display: "flex", alignItems: "center" }}
                        >
                          <label>Observaciones: </label>
                        </Grid>
                        <Grid item xl={9} lg={9} md={9} sm={12} xs={12}>
                          <TextareaAutosize
                            placeholder=""
                            value={values.observe}
                            name="observe"
                            onChange={handleChange}
                            style={{ width: 400 }}
                          />
                        </Grid>
                      </Grid>
                    </Grid>
                    <Grid item xl={4} lg={4} md={4} sm={12} xs={12}>
                      <Grid container spacing={3}>
                        <Grid
                          item
                          xl={5}
                          lg={5}
                          md={5}
                          sm={12}
                          xs={12}
                          style={{ display: "flex", alignItems: "center" }}
                        >
                          <label>Percepción aplica a partir de: </label>
                        </Grid>
                        <Grid item xl={7} lg={7} md={7} sm={12} xs={12}>
                          <label>S/. 500.0</label>
                        </Grid>
                      </Grid>
                      <Grid container spacing={3}>
                        <Grid
                          item
                          xl={5}
                          lg={5}
                          md={5}
                          sm={12}
                          xs={12}
                          style={{ display: "flex", alignItems: "center" }}
                        >
                          <FormControlLabel
                            control={
                              <Checkbox
                                checked={percentcheck}
                                onChange={handlePercentCheck}
                                name="percentcheck"
                              />
                            }
                            label="Percepción (2% ):"
                          />
                        </Grid>
                        <Grid
                          item
                          xl={7}
                          lg={7}
                          md={7}
                          sm={12}
                          xs={12}
                          style={{ display: "flex", alignItems: "center" }}
                        >
                          <label>
                            {percentcheck
                              ? Math.floor(getTotal() * 0.02 * 100) / 100
                              : "-"}
                          </label>
                        </Grid>
                      </Grid>
                      <Grid container spacing={3}>
                        <Grid
                          item
                          xl={5}
                          lg={5}
                          md={5}
                          sm={12}
                          xs={12}
                          style={{ display: "flex", alignItems: "center" }}
                        >
                          <label> Total: </label>
                        </Grid>
                        <Grid item xl={7} lg={7} md={7} sm={12} xs={12}>
                          <label>
                            S/.{" "}
                            {percentcheck
                              ? Math.floor(getTotal() * 1.2 * 100) / 100
                              : Math.floor(getTotal() * 1.18 * 100) / 100}
                          </label>
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
                  <Button
                    onClick={() => {
                      onCancel();
                    }}
                  >
                    {"Cancelar"}
                  </Button>
                  <Button
                    variant="contained"
                    type="submit"
                    disabled={editable}
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
            maxWidth="lg"
            fullWidth
            onClose={handleModalClose3}
            open={isModalOpen3}
          >
            {/* Dialog renders its body even if not open */}
            {isModalOpen3 && (
              <ItemTables
                _getCheckedValue={getCheckedValue}
                onCancel={handleModalClose3}
              />
            )}
          </Dialog>
          <Dialog
            maxWidth="md"
            fullWidth
            onClose={handleProveedorModalClose}
            open={proveedorModalOpen}
          >
            {/* Dialog renders its body even if not open */}
            {proveedorModalOpen && (
              <ProveedorTables
                _getCheckedValue={getProveedorCheckedValue}
                onCancel={handleProveedorModalClose}
              />
            )}
          </Dialog>
        </>
      )}
    </>
  );
};

export default NewOrden;
