import { useEffect, useState } from "react";
import type { FC } from "react";
import clsx from "clsx";
import PropTypes from "prop-types";
import PerfectScrollbar from "react-perfect-scrollbar";
import {
  Box,
  Button,
  Card,
  Checkbox,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
  TextField,
  makeStyles,
  Dialog,
  Grid,
  IconButton,
  FormGroup,
  FormControlLabel,
} from "@material-ui/core";
import Select from "react-select";
import * as Yup from "yup";
import { FieldArray, Formik } from "formik";
import {
  Edit as EditIcon,
  Trash as DeleteIcon,
  Search as SearchIcon,
} from "react-feather";

import SaveIcon from "@material-ui/icons/Save";
import SearchIcon2 from "@material-ui/icons/Search";
import AddIcon2 from "@material-ui/icons/Add";
import CancelIcon from "@material-ui/icons/Cancel";

import type { Theme } from "src/theme";
import useSettings from "src/hooks/useSettings";
import ConfirmModal from "src/components/ConfirmModal";
import { useSnackbar } from "notistack";
import FileCopyIcon from "@material-ui/icons/FileCopy";

import {
  deleteStyle,
  getStyle,
  getCurTallas,
  getEstiloCombList,
  getSizeList,
  getEstiloTallaID,
} from "src/apis/styleApi";
import { getCheckedTallas } from "src/apis/processApi";
import { useHistory } from "react-router-dom";
import {
  getEstiloProcess,
  getProcess,
  getEstiloData,
  saveEstiloProcess,
} from "src/apis/processApi";
import NewProcess from "./NewProcess";
import NewChildItem from "./NewChildItem";
import {
  createNoSubstitutionTemplateLiteral,
  isTemplateMiddleOrTemplateTail,
} from "typescript";
interface TablesProps {
  className?: string;
  _estilo?: any;
  onCancel?: () => void;
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
  bulkOperations: {
    position: "relative",
  },
  bulkActions: {
    paddingLeft: 4,
    paddingRight: 4,
    marginTop: 6,
    position: "absolute",
    width: "100%",
    zIndex: 2,
    backgroundColor: theme.palette.background.default,
  },
  bulkAction: {
    marginLeft: theme.spacing(2),
  },
  queryField: {
    width: 200,
  },
  queryFieldMargin: {
    marginLeft: theme.spacing(2),
  },
  categoryField: {
    width: 200,
    flexBasis: 200,
  },
  availabilityField: {
    width: 200,
    marginLeft: theme.spacing(2),
    flexBasis: 200,
  },
  buttonBox: {
    "& > *": {
      margin: theme.spacing(1),
    },
  },
  stockField: {
    marginLeft: theme.spacing(2),
  },
  shippableField: {
    marginLeft: theme.spacing(2),
  },
  imageCell: {
    fontSize: 0,
    width: 68,
    flexBasis: 68,
    flexGrow: 0,
    flexShrink: 0,
  },
  image: {
    height: 68,
    width: 68,
  },
}));

const ProcessTable: FC<TablesProps> = ({ className, _estilo, ...rest }) => {
  const classes = useStyles();
  const { enqueueSnackbar } = useSnackbar();
  const { settings, saveSettings } = useSettings();
  const history = useHistory();

  const [isModalOpen3, setIsModalOpen3] = useState(false);
  const [values, setValues] = useState({
    direction: settings.direction,
    responsiveFontSizes: settings.responsiveFontSizes,
    theme: settings.theme,
  });
  const [styles, setStyles] = useState<any>([]);
  const [filters, setFilters] = useState({
    code: "",
    name: "",
    color: "",
  });

  const [estilo, setEstilo] = useState(_estilo);

  const [tallas, setTallas] = useState<any>([]);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [deleteID, setDeleteID] = useState("-1");
  const [editID, setEditID] = useState(-1);
  const [isReplicate, setIsReplicate] = useState(0);

  const [allProcess, setAllProcess] = useState(null);
  const [estiloProcess, setEstiloProcess] = useState(null);
  const [isModalOpen2, setIsModalOpen2] = useState(false);
  const [isModalOpen1, setIsModalOpen1] = useState(false);
  const [isReplicateModalOpen, setIsReplicateModalOpen] = useState(false);
  const [selectedRowId, setSelectedRowId] = useState(null);
  const [relpicateCode, setReplicateCode] = useState("");
  const [page, setPage] = useState<number>(0);
  const [limit] = useState<number>(15);

  const [checkedValue, setCheckedValue] = useState<any>(estilo.sizeName);
  const paginatedStyles = applyPagination(styles, page, limit);

  const [estilocombos, setEstilocombos] = useState<any>([]);
  const [curEstilocombo, setCurEstilocombo] = useState<any>(-1);
  const [sizes, setSizes] = useState<any>([]);
  const [curSize, setCurSize] = useState<any>(-1);

  const [curEstilo, setCurEstilo] = useState<any>(-1);

  const selectEstilo = (v: any): void => {
    if (v != null) {
      let value = v.value;
      setCurEstilocombo(value);
      setCurEstilo(value);
      getSizeList(value)
        .then((res) => {
          setCurSize(-1);
          setSizes(res);
        })
        .catch((err) => {
          setSizes([]);
        });
    } else {
      setCurSize(-1);
      setSizes([]);
    }
  };

  const selectSize = (v: string): void => {};

  useEffect(() => {
    _getInitialData();
  }, []);

  const getChecked = (tid, sizes) => {
    for (var i = 0; i < sizes.length; i++) {
      if (sizes[i].key === tid) return true;
    }
    return false;
  };

  const makeEstiloProcess = (data) => {
    let childdatas = [];
    let res = [];
    let fres = [];
    let lstProcessIDs = [];
    data.map((item, index) => {
      if (lstProcessIDs.indexOf(item.esti_proceso_c_iid) == -1) {
        lstProcessIDs.push(item.esti_proceso_c_iid);

        res.push(item);
      } else {
        childdatas.push(item);
      }
    });

    res.map((item, index) => {
      let _childinfo = [];
      childdatas.map((childItem, index) => {
        if (childItem.esti_proceso_c_iid == item.esti_proceso_c_iid) {
          let achildItem = {};
          achildItem["vdesc"] = childItem["esti_proc_detalle_c_vdescripcion"];
          achildItem["cost"] = childItem["esti_proc_detalle_c_ecosto"];
          achildItem["effot"] = childItem["esti_proc_detalle_c_isegundos"];
          achildItem["childId"] = childItem["esti_proc_detalle_c_iid"];
          achildItem["order"] = childItem["esti_proceso_c_yorden"];
          _childinfo.push(achildItem);
        }
      });

      let _item = item;
      _item.childinfo = _childinfo;
      fres.push(_item);
    });

    setEstiloProcess(fres);
  };

  const _getReplicatelData = async () => {
    let tallaID = 0;
    await getEstiloTallaID({
      id1: curEstilo.toString(),
      id2: curSize?.label,
    }).then((res: number) => {
      tallaID = res;
    });

    getEstiloProcess({
      id1: curEstilo.toString(),
      id2: tallaID.toString(),
    })
      .then((res: any[]) => {
        makeEstiloProcess(res);
      })
      .catch((err) => {
        setEstiloProcess([]);
      });

    getProcess()
      .then((res: any[]) => {
        setAllProcess(res);
      })
      .catch((err) => {
        setAllProcess([]);
      });

    handleSearch();
  };

  const handleReplicateData = async () => {
    setIsReplicate(1);

    _getReplicatelData();
  };

  const handleReplicate = () => {
    let code = "";
    setIsReplicateModalOpen(true);
  };

  const _getInitialData = () => {
    getEstiloCombList("")
      .then((res: any) => {
        setEstilocombos(res);
      })
      .catch((err) => {});

    let checkedValue = [{ key: _estilo.estilo_talla_c_vid, check: true }];

    getEstiloProcess({
      id1: estilo.estilo_c_iid.toString(),
      id2: estilo.estilo_talla_c_iid.toString(),
    })
      .then((res: any[]) => {
        makeEstiloProcess(res);
      })
      .catch((err) => {
        setEstiloProcess([]);
      });
    getProcess()
      .then((res: any[]) => {
        setAllProcess(res);
      })
      .catch((err) => {
        setAllProcess([]);
      });

    getCurTallas(_estilo.estilo_c_iid)
      .then((res: any[]) => {
        var _sizes = [];
        for (var i = 0; i < res.length; i++) {
          _sizes.push({
            id: i,
            key: res[i].talla_c_vid,
            description: res[i].talla_c_vdescripcion,
            check: getChecked(res[i].talla_c_vid, checkedValue),
          });
        }
        setTallas(_sizes);
      })
      .catch((err) => {
        setTallas([]);
      });
    handleSearch();
  };

  const getInitialValues = () => {
    return {
      id: "-1",
      isReplicate: 0,
      estilo_c_iid: _estilo.estilo_c_iid,
      estiloProcesses: estiloProcess,
      submit: null,
    };
  };

  const handleModalClose = (): void => {
    setIsModalOpen(false);
  };
  const handleModalOpen3 = (): void => {
    setIsModalOpen3(true);
  };
  const handleModalClose1 = (): void => {
    setIsModalOpen1(false);
  };
  const handleModalOpen1 = (id: number): void => {
    setIsModalOpen1(true);
    setSelectedRowId(id);
  };
  const handleSearch = () => {
    getStyle(filters)
      .then((res) => {
        setStyles(res);
      })
      .catch((err) => {
        setStyles([]);
      });
  };

  const handleMultiSelectorChange = (e: any): void => {
    if (e.target.name.indexOf(".check") > -1) {
      var data = { [e.target.name]: e.target.checked };

      let newSize = tallas.map((item, index) => {
        if (
          index == e.target.name.replace("sizes[", "").replace("].check", "")
        ) {
          item.check = e.target.checked;
        }
        return item;
      });
      setCheckedValue(newSize);
      //setEstilo({ ...estilo, sizeName: newSize });
    }
  };

  const addEstiloProcess = () => {};

  const handleSave = () => {};

  const handleCancel = () => {};
  const handlePageChange = (event: any, newPage: number): void => {
    setPage(newPage);
  };

  const sizeString = (sizes) => {
    var res = "";
    for (var i = 0; i < sizes.length; i++) {
      res += (i > 0 ? ", " : "") + sizes[i].key;
    }
    return res;
  };

  const handleModalClose3 = (): void => {
    setIsModalOpen3(false);
    _getInitialData();
  };

  const getTotal = (values, type) => {
    var sum = 0;
    if (type === 0) {
      for (var i = 0; i < estiloProcess.length; i++) {
        if (
          estiloProcess[i].childinfo != null &&
          estiloProcess[i].childinfo.length > 0
        ) {
          for (var j = 0; j < estiloProcess[i].childinfo.length; j++) {
            sum += estiloProcess[i].childinfo[j].cost * 1;
          }
        } else {
          sum += estiloProcess[i].esti_proc_detalle_c_ecosto * 1;
        }
      }
      return Math.round(sum * 100) / 100;
    } else {
      for (var i = 0; i < estiloProcess.length; i++) {
        if (
          estiloProcess[i].childinfo != null &&
          estiloProcess[i].childinfo.length > 0
        ) {
          for (var j = 0; j < estiloProcess[i].childinfo.length; j++) {
            sum += estiloProcess[i].childinfo[j].effot * 1;
          }
        } else {
          sum += estiloProcess[i].esti_proc_detalle_c_isegundos * 1;
        }
      }
      return Math.round(sum * 100) / 100;
    }
  };

  const handleHeaderChange = (e: any): void => {
    // var data = { [e.target.name]: e.target.value };
    // setEstilo({ ...estilo, ...data });
  };

  const getSubTotal = (estiloProcesses, type) => {
    var sum = 0;
    if (estiloProcesses.childinfo == null) {
      return 0;
    }
    if (type === 0) {
      for (var i = 0; i < estiloProcesses.childinfo.length; i++) {
        sum += estiloProcesses.childinfo[i].cost * 1;
      }
      return Math.round(sum * 100) / 100;
    } else {
      for (var i = 0; i < estiloProcesses.childinfo.length; i++) {
        sum += estiloProcesses.childinfo[i].effot * 1;
      }
      return Math.round(sum * 100) / 100;
    }
  };

  return (
    <Card className={clsx(classes.root, className)} {...rest}>
      <PerfectScrollbar>
        {estiloProcess && (
          <Box minWidth={1200} style={{ paddingBottom: 40 }}>
            <Formik
              initialValues={getInitialValues()}
              validationSchema={Yup.object().shape({
                estiloProcesses: Yup.array().of(
                  Yup.object().shape({
                    proceso_c_vid: Yup.mixed().notOneOf(
                      ["-1"],
                      "Este campo es requerido."
                    ),
                    // esti_proc_detalle_c_vdescripcion: Yup.string()
                    //   .max(50, "Debe tener 50 caracteres como máximo")
                    //   .required("Se requiere una razón social"),
                    // esti_proc_detalle_c_ecosto: Yup.number()
                    //   .required("Este campo es requerido")
                    //   .positive("Este campo es requerido"),
                    // esti_proc_detalle_c_isegundos: Yup.number()
                    //   .required("Este campo es requerido")
                    //   .positive("Este campo es requerido"),
                  })
                ),
              })}
              onSubmit={async (
                values,
                { resetForm, setErrors, setStatus, setSubmitting }
              ) => {
                saveSettings({ saving: true });
                values["sizes"] = tallas;
                values["estiloProcesses"] = estiloProcess;
                if (isReplicate == 1) {
                  values["isReplicate"] = 1;
                  values["estilo"] = estilo;
                } else {
                  values["isReplicate"] = 0;
                }
                values["estilo_talla_c_iid"] = estilo.estilo_talla_c_iid;
                if (values.estiloProcesses?.length == 0) {
                  enqueueSnackbar("Por favor, crear por lo menos un proceso", {
                    variant: "error",
                  });
                  saveSettings({ saving: false });
                  return;
                }
                window.setTimeout(() => {
                  saveEstiloProcess(values)
                    .then((res) => {
                      saveSettings({ saving: false });
                      // _getInitialData();
                      enqueueSnackbar(
                        "Tus datos se han guardado exitosamente.",
                        {
                          variant: "success",
                        }
                      );
                      resetForm();
                      setStatus({ success: true });
                      setSubmitting(false);
                      window.location.href = "";

                      // if (isReplicate == 1) {
                      //   history.goBack();
                      // }
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
                  <Box p={3} alignItems="center">
                    <Grid container spacing={3}>
                      <Grid item lg={12} sm={12} xs={12}>
                        <Grid container spacing={3}>
                          <Grid
                            item
                            lg={6}
                            sm={6}
                            xs={12}
                            style={{ display: "flex", alignItems: "center" }}
                          >
                            <TextField
                              fullWidth
                              size="small"
                              label="Estilo"
                              placeholder="Estilo"
                              variant="outlined"
                              name="estilo"
                              value={
                                estilo
                                  ? estilo.estilo_c_vcodigo +
                                    " - " +
                                    estilo.estilo_c_vnombre +
                                    " - " +
                                    estilo.estilo_talla_c_vid
                                  : ""
                              }
                              // onChange={(e) => setFilters({...filters, code: e.target.value})}
                              onChange={(e) => {
                                setFieldValue("estilo", e.target.value);
                              }}
                              InputLabelProps={{
                                shrink: true,
                              }}
                            />
                          </Grid>
                          <Grid
                            item
                            style={{ display: "flex", alignItems: "center" }}
                          >
                            <Select
                              isClearable
                              name="Estilos"
                              options={estilocombos}
                              placeholder="Estilo"
                              className="basic-multi-select"
                              classNamePrefix="Estilo"
                              menuPortalTarget={document.body}
                              styles={{
                                menuPortal: (base) => ({
                                  ...base,
                                  zIndex: 9999,
                                }),
                                control: (base) => ({
                                  ...base,
                                  width: 200,
                                }),
                              }}
                              onChange={(e) => {
                                selectEstilo(e);
                              }}
                            />
                          </Grid>
                          <Grid
                            item
                            style={{ display: "flex", alignItems: "center" }}
                          >
                            <Select
                              isClearable
                              name="Sizes"
                              options={sizes}
                              value={curSize}
                              placeholder="Size"
                              className="basic-multi-select"
                              classNamePrefix="estilo"
                              menuPortalTarget={document.body}
                              styles={{
                                menuPortal: (base) => ({
                                  ...base,
                                  zIndex: 9999,
                                }),
                                control: (base) => ({
                                  ...base,
                                  width: 200,
                                }),
                              }}
                              onChange={(e) => {
                                setCurSize(e);
                                selectSize(e ? e.value : -1);
                              }}
                            />
                          </Grid>
                          <Grid
                            item
                            style={{ display: "flex", alignItems: "center" }}
                          >
                            <Box>
                              <Button
                                size="small"
                                color="primary"
                                aria-label="add to shopping cart"
                                variant="contained"
                                onClick={() => {
                                  handleReplicate();
                                }}
                              >
                                {"Replicar"}
                              </Button>
                            </Box>
                          </Grid>
                        </Grid>
                      </Grid>
                    </Grid>
                  </Box>
                  <Grid container spacing={5}>
                    <Grid
                      item
                      style={{ display: "flex", alignItems: "center" }}
                      xl={2}
                      lg={2}
                      md={2}
                      sm={2}
                      xs={2}
                    >
                      <Box p={3}>
                        <Button
                          onClick={() => {
                            handleSearch();
                            handleModalOpen3();
                          }}
                          variant="contained"
                          color="primary"
                          startIcon={<AddIcon2 />}
                        >
                          {"Agregar Proceso"}
                        </Button>
                      </Box>
                    </Grid>
                    <Grid
                      item
                      style={{ display: "flex", alignItems: "center" }}
                      xl={2}
                      lg={2}
                      md={2}
                      sm={2}
                      xs={2}
                    >
                      <Box p={3}>
                        <Button
                          onClick={() => {
                            addEstiloProcess();
                            var _temp = estiloProcess;
                            _temp.push({
                              _id: -1,
                              esti_proceso_c_iid: 0,
                              proceso_c_vid: "-1",
                              esti_proc_detalle_c_vdescripcion: "",
                              esti_proc_detalle_c_ecosto: 0,
                              esti_proc_detalle_c_isegundos: 0,
                            });
                            setFieldValue("estiloProcesses", _temp);
                          }}
                          variant="contained"
                          color="primary"
                          startIcon={<AddIcon2 />}
                        >
                          {"Añadir"}
                        </Button>
                      </Box>
                    </Grid>
                    <Grid
                      item
                      style={{ display: "flex", alignItems: "center" }}
                      xl={2}
                      lg={2}
                      md={2}
                      sm={2}
                      xs={2}
                    >
                      <Box p={3}>
                        <Button
                          type="submit"
                          disabled={isSubmitting}
                          variant="contained"
                          color="secondary"
                          startIcon={<SaveIcon />}
                        >
                          {"Guardar"}
                        </Button>
                      </Box>
                    </Grid>
                    <Grid
                      item
                      style={{ display: "flex", alignItems: "center" }}
                      xl={2}
                      lg={2}
                      md={2}
                      sm={2}
                      xs={2}
                    >
                      <Box p={3}>
                        <Button
                          onClick={() => {
                            history.goBack();
                          }}
                          variant="contained"
                          startIcon={<CancelIcon />}
                        >
                          {"Cancelar"}
                        </Button>
                      </Box>
                    </Grid>
                    <Grid
                      item
                      xl={3}
                      lg={3}
                      md={3}
                      sm={3}
                      xs={3}
                      style={{ display: "flex", alignItems: "center" }}
                    >
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
                          {tallas && tallas.length > 0 ? (
                            <FormGroup>
                              {tallas.map((t, i) => (
                                <FormControlLabel
                                  key={i.toString()}
                                  control={
                                    <Checkbox
                                      checked={tallas[i].check}
                                      disabled={
                                        tallas[i].key ==
                                        estilo.estilo_talla_c_vid
                                          ? true
                                          : false
                                      }
                                      onChange={handleMultiSelectorChange}
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

                  <Table stickyHeader>
                    <TableHead style={{ background: "red" }}>
                      <TableRow>
                        <TableCell>Código</TableCell>
                        <TableCell>Descripción</TableCell>
                        <TableCell>Costo*</TableCell>
                        <TableCell>Esfuerzo en '</TableCell>
                      </TableRow>
                    </TableHead>
                    <TableBody>
                      {estiloProcess.map((process, index) => (
                        <>
                          <TableRow
                            style={{ height: 30 }}
                            hover
                            // key={item.estilo_c_iid}
                            key={index.toString()}
                          >
                            <TableCell>
                              {/* {process.proceso_c_vid} */}
                              <TextField
                                size="small"
                                style={{
                                  minWidth: 100,
                                }}
                                name={`estiloProcesses[${index}].proceso_c_vid`}
                                error={Boolean(
                                  touched.estiloProcesses &&
                                    touched.estiloProcesses[index] &&
                                    touched.estiloProcesses[index]
                                      .proceso_c_vid &&
                                    errors.estiloProcesses &&
                                    errors.estiloProcesses[index] &&
                                    errors.estiloProcesses[index][
                                      "proceso_c_vid"
                                    ]
                                )}
                                helperText={
                                  <>
                                    {touched.estiloProcesses &&
                                      touched.estiloProcesses[index] &&
                                      touched.estiloProcesses[index]
                                        .proceso_c_vid &&
                                      errors.estiloProcesses &&
                                      errors.estiloProcesses[index] &&
                                      errors.estiloProcesses[index][
                                        "proceso_c_vid"
                                      ]}
                                  </>
                                }
                                SelectProps={{ native: true }}
                                select
                                onBlur={handleBlur}
                                value={estiloProcess[index].proceso_c_vid}
                                onChange={(e) => {
                                  var _temp = estiloProcess;
                                  _temp[index].proceso_c_vid = e.target.value;
                                  setFieldValue("estiloProcesses", _temp);
                                }}
                              >
                                <option defaultValue="-1" key="-1" value="-1">
                                  {"-- Seleccionar --"}
                                </option>
                                {allProcess &&
                                  allProcess.length > 0 &&
                                  allProcess.map((p, index2) => (
                                    <option
                                      key={index2.toString()}
                                      value={p.proceso_c_vid}
                                    >
                                      {p.proceso_c_vdescripcion}
                                    </option>
                                  ))}
                              </TextField>
                            </TableCell>
                            <TableCell>
                              <label>
                                {(() => {
                                  var descData =
                                    allProcess &&
                                    allProcess.length > 0 &&
                                    allProcess.find(
                                      (item) =>
                                        item.proceso_c_vid ==
                                        estiloProcess[index].proceso_c_vid
                                    );
                                  return descData?.proceso_c_vid ?? "";
                                })()}
                              </label>
                            </TableCell>
                            <TableCell>
                              <TextField
                                style={{
                                  width: 150,
                                }}
                                name={`estiloProcesses[${index}].esti_proc_detalle_c_ecosto`}
                                error={Boolean(
                                  touched.estiloProcesses &&
                                    touched.estiloProcesses[index] &&
                                    touched.estiloProcesses[index]
                                      .esti_proc_detalle_c_ecosto &&
                                    errors.estiloProcesses &&
                                    errors.estiloProcesses[index] &&
                                    errors.estiloProcesses[index][
                                      "esti_proc_detalle_c_ecosto"
                                    ]
                                )}
                                type="number"
                                fullWidth
                                helperText={
                                  <>
                                    {touched.estiloProcesses &&
                                      touched.estiloProcesses[index] &&
                                      touched.estiloProcesses[index]
                                        .esti_proc_detalle_c_ecosto &&
                                      errors.estiloProcesses &&
                                      errors.estiloProcesses[index] &&
                                      errors.estiloProcesses[index][
                                        "esti_proc_detalle_c_ecosto"
                                      ]}
                                  </>
                                }
                                onBlur={handleBlur}
                                value={
                                  estiloProcess[index]?.childinfo?.length > 0
                                    ? getSubTotal(estiloProcess[index], 0)
                                    : estiloProcess[index]
                                        .esti_proc_detalle_c_ecosto
                                }
                                onChange={(e) => {
                                  var _temp = estiloProcess;
                                  _temp[index].esti_proc_detalle_c_ecosto =
                                    e.target.value;
                                  setFieldValue("estiloProcesses", _temp);
                                }}
                              />
                            </TableCell>
                            <TableCell>
                              <TextField
                                style={{
                                  width: 150,
                                }}
                                name={`estiloProcesses[${index}].esti_proc_detalle_c_isegundos`}
                                error={Boolean(
                                  touched.estiloProcesses &&
                                    touched.estiloProcesses[index] &&
                                    touched.estiloProcesses[index]
                                      .esti_proc_detalle_c_isegundos &&
                                    errors.estiloProcesses &&
                                    errors.estiloProcesses[index] &&
                                    errors.estiloProcesses[index][
                                      "esti_proc_detalle_c_isegundos"
                                    ]
                                )}
                                type="number"
                                fullWidth
                                helperText={
                                  <>
                                    {touched.estiloProcesses &&
                                      touched.estiloProcesses[index] &&
                                      touched.estiloProcesses[index]
                                        .esti_proc_detalle_c_isegundos &&
                                      errors.estiloProcesses &&
                                      errors.estiloProcesses[index] &&
                                      errors.estiloProcesses[index][
                                        "esti_proc_detalle_c_isegundos"
                                      ]}
                                  </>
                                }
                                onBlur={handleBlur}
                                value={
                                  estiloProcess[index]?.childinfo?.length > 0
                                    ? getSubTotal(estiloProcess[index], 1)
                                    : estiloProcess[index]
                                        .esti_proc_detalle_c_isegundos
                                }
                                onChange={(e) => {
                                  var _temp = estiloProcess;
                                  _temp[index].esti_proc_detalle_c_isegundos =
                                    e.target.value;
                                  setFieldValue("estiloProcesses", _temp);
                                }}
                              />
                            </TableCell>
                            <TableCell>
                              <Button
                                onClick={() => {
                                  handleModalOpen1(index);
                                }}
                                variant="contained"
                                color="primary"
                              >
                                <AddIcon2 />
                              </Button>
                            </TableCell>
                          </TableRow>
                          {process.childinfo &&
                            process.childinfo.map((childitem, childindex) => {
                              return (
                                <TableRow
                                  style={{ height: 30 }}
                                  hover
                                  // key={item.estilo_c_iid}
                                  key={index.toString()}
                                >
                                  <TableCell />
                                  <TableCell>
                                    <TextField
                                      name={`estiloProcesses[${index}].childinfo[${childindex}].description`}
                                      fullWidth
                                      onBlur={handleBlur}
                                      value={childitem.vdesc}
                                      onChange={(e) => {
                                        var _temp = estiloProcess;
                                        _temp[index].childinfo[
                                          childindex
                                        ].vdesc = e.target.value;
                                        setFieldValue("estiloProcesses", _temp);
                                      }}
                                    />
                                  </TableCell>
                                  <TableCell>
                                    <TextField
                                      style={{
                                        width: 150,
                                      }}
                                      name={`estiloProcesses[${index}].childinfo[${childindex}].cost`}
                                      type="number"
                                      fullWidth
                                      onBlur={handleBlur}
                                      value={childitem.cost}
                                      onChange={(e) => {
                                        var _temp = estiloProcess;
                                        _temp[index].childinfo[
                                          childindex
                                        ].cost = e.target.value;
                                        setFieldValue("estiloProcesses", _temp);
                                      }}
                                    />
                                  </TableCell>
                                  <TableCell>
                                    <TextField
                                      style={{
                                        width: 150,
                                      }}
                                      name={`estiloProcesses[${index}].childinfo[${childindex}].effot`}
                                      type="number"
                                      fullWidth
                                      onBlur={handleBlur}
                                      value={childitem.effot}
                                      onChange={(e) => {
                                        var _temp = estiloProcess;
                                        _temp[index].childinfo[
                                          childindex
                                        ].effot = e.target.value;
                                        setFieldValue("estiloProcesses", _temp);
                                      }}
                                    />
                                  </TableCell>
                                  <TableCell />
                                </TableRow>
                              );
                            })}
                        </>
                      ))}
                      <TableRow style={{ height: 30 }} hover>
                        <TableCell></TableCell>
                        <TableCell>TOTAL</TableCell>
                        <TableCell>{getTotal(values, 0)}</TableCell>
                        <TableCell>{getTotal(values, 1)}</TableCell>
                      </TableRow>
                    </TableBody>
                  </Table>
                  <Dialog
                    maxWidth="md"
                    fullWidth
                    onClose={handleModalClose1}
                    open={isModalOpen1}
                  >
                    {/* Dialog renders its body even if not open */}
                    {isModalOpen1 && (
                      <NewChildItem
                        onCancel={handleModalClose1}
                        handleAddChild={(childinfo) => {
                          var _temp = estiloProcess;
                          if (!_temp[selectedRowId].childinfo) {
                            _temp[selectedRowId].childinfo = [];
                          }
                          childinfo["childId"] = -1;
                          _temp[selectedRowId].childinfo.push(childinfo);
                          setFieldValue("estiloProcesses", _temp);
                        }}
                      />
                    )}
                  </Dialog>
                </form>
              )}
            </Formik>

            <Dialog
              maxWidth="md"
              fullWidth
              onClose={handleModalClose3}
              open={isModalOpen3}
            >
              {/* Dialog renders its body even if not open */}
              {isModalOpen3 && (
                <NewProcess
                  onAddComplete={handleModalClose3}
                  onCancel={handleModalClose3}
                  onDeleteComplete={handleModalClose3}
                  onEditComplete={handleModalClose3}
                />
              )}
            </Dialog>
          </Box>
        )}
      </PerfectScrollbar>
      <ConfirmModal
        open={isReplicateModalOpen}
        title={
          "¿Está seguro de replicar los procesos del estilo " +
          relpicateCode +
          "?, en caso si, se elimina toda la información ingresada y se reemplaza por todos los datos de los procesos del estilo " +
          relpicateCode
        }
        setOpen={() => setIsReplicateModalOpen(false)}
        onConfirm={() => {
          handleReplicateData();
          setIsReplicateModalOpen(false);
        }}
      />
      <ConfirmModal
        open={isModalOpen2}
        title={"¿Eliminar este artículo?"}
        setOpen={() => setIsModalOpen2(false)}
        onConfirm={() => {
          saveSettings({ saving: true });
          deleteStyle(deleteID)
            .then((res) => {
              saveSettings({ saving: false });
              handleSearch();
              enqueueSnackbar("Tus datos se han guardado exitosamente.", {
                variant: "success",
              });

              setIsModalOpen2(false);
              handleSearch();
            })
            .catch((err) => {
              setIsModalOpen2(false);
              handleSearch();
              enqueueSnackbar("No se pudo guardar.", {
                variant: "error",
              });
              saveSettings({ saving: false });
            });
        }}
      />
    </Card>
  );
};

ProcessTable.propTypes = {
  className: PropTypes.string,
};

ProcessTable.defaultProps = {};

export default ProcessTable;
