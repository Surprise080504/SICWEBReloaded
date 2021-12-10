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
  FormGroup,
  FormControlLabel,
  InputAdornment,
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
import NewItem from "../../maintenance/ItemView/NewItem";
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
  getEstiloInsumos,
  getItems,
  getEstiloData,
  saveEstiloInsumos,
} from "src/apis/insumosApi";
import { getUnits } from "src/apis/itemApi";
import {
  createNoSubstitutionTemplateLiteral,
  isTemplateMiddleOrTemplateTail,
} from "typescript";
import da from "date-fns/esm/locale/da/index.js";
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

const InsumosTable: FC<TablesProps> = ({ className, _estilo, ...rest }) => {
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

  const [allItems, setAllItems] = useState<any>([]);
  const [codigoItems, setCodigoItems] = useState<any>([]);

  const [tallas, setTallas] = useState<any>([]);
  const [deleteID, setDeleteID] = useState("-1");
  const [isReplicate, setIsReplicate] = useState(0);

  const [estiloInsumos, setEstiloInsumos] = useState(null);
  const [isModalOpen2, setIsModalOpen2] = useState(false);
  const [isReplicateModalOpen, setIsReplicateModalOpen] = useState(false);
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
  const [isModalOpen, setIsModalOpen] = useState(false);

  const [units, setUnits] = useState<any>([]);
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
    _getUnits();
  }, []);

  const getChecked = (tid, sizes) => {
    for (var i = 0; i < sizes.length; i++) {
      if (sizes[i].key === tid) return true;
    }
    return false;
  };

  const convertCodigos = (items: any[]) => {
    let lstCodigos = [];
    items.map((item) => {
      let tmp = {
        value: item.id,
        label: item.code,
      };

      lstCodigos.push(tmp);
    });

    return lstCodigos;
  };

  const _getReplicatelData = async () => {
    let tallaID = 0;
    await getEstiloTallaID({
      id1: curEstilo.toString(),
      id2: curSize?.label,
    }).then((res: number) => {
      tallaID = res;
    });

    await getEstiloInsumos({
      id1: curEstilo.toString(),
      id2: tallaID.toString(),
    })
      .then((res: any[]) => {
        setEstiloInsumos(res);
      })
      .catch((err) => {
        setEstiloInsumos([]);
      });

    let tmpItems = [];
    await getItems()
      .then((res: any[]) => {
        tmpItems = res;
        setAllItems(res);
      })
      .catch((err) => {
        setAllItems([]);
      });

    setCodigoItems(convertCodigos(tmpItems));
    handleSearch();
  };

  const handleReplicateData = async () => {
    setIsReplicate(1);
    _getReplicatelData();
  };

  const handleRefreshcodigoItems = async () => {
    let tmpItems = [];
    await getItems()
      .then((res: any[]) => {
        tmpItems = res;
        setAllItems(res);
      })
      .catch((err) => {
        setAllItems([]);
      });

    setCodigoItems(convertCodigos(tmpItems));
  };

  const handleModalClose = (): void => {
    setIsModalOpen(false);
    handleRefreshcodigoItems();
  };

  const handleModalOpen = (): void => {
    setIsModalOpen(true);
  };

  const handleReplicate = () => {
    setIsReplicateModalOpen(true);
  };

  const _getInitialData = async () => {
    getEstiloCombList("")
      .then((res: any) => {
        setEstilocombos(res);
      })
      .catch((err) => {});

    let checkedValue = [{ key: _estilo.estilo_talla_c_vid, check: true }];

    getEstiloInsumos({
      id1: estilo.estilo_c_iid.toString(),
      id2: estilo.estilo_talla_c_iid.toString(),
    })
      .then((res: any[]) => {
        setEstiloInsumos(res);
      })
      .catch((err) => {
        setEstiloInsumos([]);
      });
    let tempItems = [];
    await getItems()
      .then((res: any[]) => {
        tempItems = res;
        setAllItems(res);
      })
      .catch((err) => {
        setAllItems([]);
      });

    setCodigoItems(convertCodigos(tempItems));

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

  const _getUnits = () => {
    getUnits().then((res) => {
      setUnits(res);
    });
  };

  const getInitialValues = () => {
    return {
      id: "-1",
      estilo_c_iid: _estilo.estilo_c_iid,
      estiloInsumoses: estiloInsumos,
      submit: null,
    };
  };
  const handleSearch = async () => {
    await getStyle(filters)
      .then((res) => {
        setStyles(res);
      })
      .catch((err) => {
        setStyles([]);
      });

    await getEstiloInsumos({
      id1: estilo.estilo_c_iid.toString(),
      id2: estilo.estilo_talla_c_iid.toString(),
    })
      .then((res: any[]) => {
        setEstiloInsumos(res);
      })
      .catch((err) => {
        setEstiloInsumos([]);
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

  const addEstiloInsumos = () => {};

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

  const getTotal = (values) => {
    var sum = 0;

    for (var i = 0; i < estiloInsumos.length; i++) {
      sum +=
        Number(estiloInsumos[i].estilo_insumo_c_ecosto) *
        Number(estiloInsumos[i].estilo_insumo_c_emerma) *
        (1 + Number(estiloInsumos[i].estilo_insumo_c_econsumo) / 100);
    }
    return Math.round(sum * 100) / 100;
  };

  const getRowTotal = (data) => {
    var sum =
      Number(data.estilo_insumo_c_ecosto) *
      Number(data.estilo_insumo_c_emerma) *
      (1 + Number(data.estilo_insumo_c_econsumo) / 100);
    return Math.round(sum * 100) / 100;
  };

  const handleHeaderChange = (e: any): void => {
    var data = { [e.target.name]: e.target.value };

    setEstilo({ ...estilo, ...data });
  };

  return (
    <Card className={clsx(classes.root, className)} {...rest}>
      <PerfectScrollbar>
        {estiloInsumos && (
          <Box minWidth={1200} style={{ paddingBottom: 40 }}>
            <Formik
              initialValues={getInitialValues()}
              // validationSchema={Yup.object().shape({
              //   estiloInsumoses: Yup.array().of(
              //     Yup.object().shape({
              //       proceso_c_vid: Yup.mixed().notOneOf(
              //         ["-1"],
              //         "Este campo es requerido."
              //       ),
              // esti_proc_detalle_c_vdescripcion: Yup.string()
              //   .max(50, "Debe tener 50 caracteres como máximo")
              //   .required("Se requiere una razón social"),
              // esti_proc_detalle_c_ecosto: Yup.number()
              //   .required("Este campo es requerido")
              //   .positive("Este campo es requerido"),
              // esti_proc_detalle_c_isegundos: Yup.number()
              //   .required("Este campo es requerido")
              //   .positive("Este campo es requerido"),
              //     })
              //   ),
              // })}
              onSubmit={async (
                values,
                { resetForm, setErrors, setStatus, setSubmitting }
              ) => {
                saveSettings({ saving: true });
                values["sizes"] = tallas;
                values["estiloInsumoses"] = estiloInsumos;
                if (isReplicate == 1) {
                  values["isReplicate"] = 1;
                  values["estilo"] = estilo;
                } else {
                  values["isReplicate"] = 0;
                }
                values["estilo_talla_c_iid"] = estilo.estilo_talla_c_iid;
                if (values.estiloInsumoses?.length == 0) {
                  enqueueSnackbar("Por favor, crear por lo menos un insumo", {
                    variant: "error",
                  });
                  saveSettings({ saving: false });
                  return;
                }
                window.setTimeout(() => {
                  saveEstiloInsumos(values)
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
                          {/* <Grid
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
                            <a
                              href="/Interfaces/Mantenimiento/frmRegItem"
                              target="_blank"
                            >
                              Agregar Item
                            </a>
                          </Grid> */}
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
                            handleModalOpen();
                          }}
                          variant="contained"
                          color="primary"
                          startIcon={<AddIcon2 />}
                        >
                          {"Agregar artículo"}
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
                            addEstiloInsumos();
                            var _temp = estiloInsumos;
                            _temp.push({
                              _id: -1,
                              esti_insumo_c_iid: -1,
                              estilo_c_iid: 0,
                              itm_c_iid: 0,
                              estilo_insumo_c_ecosto: 0,
                              estilo_insumo_c_econsumo: 0,
                              estilo_insumo_c_emerma: 0,
                            });
                            setFieldValue("estiloInsumoses", _temp);
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
                        <TableCell>U. de medida</TableCell>
                        <TableCell>Costo Unitario</TableCell>
                        <TableCell>Merma</TableCell>
                        <TableCell>Consumo</TableCell>
                        <TableCell>Total</TableCell>
                      </TableRow>
                    </TableHead>
                    <TableBody>
                      {estiloInsumos.map((Insumos, index) => (
                        <>
                          <TableRow
                            style={{ height: 30 }}
                            hover
                            // key={item.estilo_c_iid}
                            key={index.toString()}
                          >
                            <TableCell>
                              <Select
                                isClearable
                                name="`estiloInsumoses[${index}].itm_c_iid`"
                                options={codigoItems}
                                placeholder=""
                                className="basic-multi-select"
                                classNamePrefix="estiloInsumoses"
                                menuPortalTarget={document.body}
                                styles={{
                                  menuPortal: (base) => ({
                                    ...base,
                                    zIndex: 9999,
                                  }),
                                  control: (base) => ({
                                    ...base,
                                    width: 500,
                                  }),
                                }}
                                value={codigoItems.filter(
                                  (item) =>
                                    item.value == estiloInsumos[index].itm_c_iid
                                )}
                                onChange={(e: any) => {
                                  if (e?.value != null) {
                                    const selectedItem = allItems.filter(
                                      (item) => item.id == e.value
                                    );
                                    var _temp = estiloInsumos;
                                    _temp[index].itm_c_iid = e.value;
                                    _temp[index].code_description =
                                      selectedItem.description;
                                    _temp[index].unit = selectedItem.unit;

                                    setFieldValue("estiloInsumoses", _temp);
                                  }
                                }}
                              />
                            </TableCell>
                            <TableCell>
                              <label>
                                {(() => {
                                  var descData = allItems.find(
                                    (item) =>
                                      item.id == estiloInsumos[index].itm_c_iid
                                  );
                                  return descData?.description ?? "";
                                })()}
                              </label>
                            </TableCell>
                            <TableCell>
                              <label>
                                {(() => {
                                  var descData = allItems.find(
                                    (item) =>
                                      item.id == estiloInsumos[index].itm_c_iid
                                  );
                                  return descData?.unit ?? "";
                                })()}
                              </label>
                            </TableCell>

                            <TableCell>
                              <TextField
                                style={{
                                  width: 150,
                                }}
                                name={`estiloInsumoses[${index}].estilo_insumo_c_ecosto`}
                                error={Boolean(
                                  touched.estiloInsumoses &&
                                    touched.estiloInsumoses[index] &&
                                    touched.estiloInsumoses[index]
                                      .estilo_insumo_c_ecosto &&
                                    errors.estiloInsumoses &&
                                    errors.estiloInsumoses[index] &&
                                    errors.estiloInsumoses[index][
                                      "estilo_insumo_c_ecosto"
                                    ]
                                )}
                                type="number"
                                fullWidth
                                helperText={
                                  <>
                                    {touched.estiloInsumoses &&
                                      touched.estiloInsumoses[index] &&
                                      touched.estiloInsumoses[index]
                                        .estilo_insumo_c_ecosto &&
                                      errors.estiloInsumoses &&
                                      errors.estiloInsumoses[index] &&
                                      errors.estiloInsumoses[index][
                                        "estilo_insumo_c_ecosto"
                                      ]}
                                  </>
                                }
                                onBlur={handleBlur}
                                value={
                                  estiloInsumos[index].estilo_insumo_c_ecosto
                                }
                                onChange={(e) => {
                                  var _temp = estiloInsumos;
                                  _temp[index].estilo_insumo_c_ecosto =
                                    e.target.value;
                                  setFieldValue("estiloInsumoses", _temp);
                                }}
                              />
                            </TableCell>
                            <TableCell>
                              <TextField
                                style={{
                                  width: 150,
                                }}
                                InputProps={{
                                  endAdornment: (
                                    <InputAdornment position="end">
                                      %
                                    </InputAdornment>
                                  ),
                                }}
                                name={`estiloInsumoses[${index}].estilo_insumo_c_econsumo`}
                                error={Boolean(
                                  touched.estiloInsumoses &&
                                    touched.estiloInsumoses[index] &&
                                    touched.estiloInsumoses[index]
                                      .estilo_insumo_c_econsumo &&
                                    errors.estiloInsumoses &&
                                    errors.estiloInsumoses[index] &&
                                    errors.estiloInsumoses[index][
                                      "estilo_insumo_c_econsumo"
                                    ]
                                )}
                                type="number"
                                fullWidth
                                helperText={
                                  <>
                                    {touched.estiloInsumoses &&
                                      touched.estiloInsumoses[index] &&
                                      touched.estiloInsumoses[index]
                                        .estilo_insumo_c_econsumo &&
                                      errors.estiloInsumoses &&
                                      errors.estiloInsumoses[index] &&
                                      errors.estiloInsumoses[index][
                                        "estilo_insumo_c_econsumo"
                                      ]}
                                  </>
                                }
                                onBlur={handleBlur}
                                value={
                                  estiloInsumos[index].estilo_insumo_c_econsumo
                                }
                                onChange={(e) => {
                                  var _temp = estiloInsumos;
                                  _temp[index].estilo_insumo_c_econsumo =
                                    e.target.value;
                                  setFieldValue("estiloInsumoses", _temp);
                                }}
                              />
                            </TableCell>
                            <TableCell>
                              <TextField
                                style={{
                                  width: 150,
                                }}
                                name={`estiloInsumoses[${index}].estilo_insumo_c_emerma`}
                                error={Boolean(
                                  touched.estiloInsumoses &&
                                    touched.estiloInsumoses[index] &&
                                    touched.estiloInsumoses[index]
                                      .estilo_insumo_c_emerma &&
                                    errors.estiloInsumoses &&
                                    errors.estiloInsumoses[index] &&
                                    errors.estiloInsumoses[index][
                                      "estilo_insumo_c_emerma"
                                    ]
                                )}
                                type="number"
                                fullWidth
                                helperText={
                                  <>
                                    {touched.estiloInsumoses &&
                                      touched.estiloInsumoses[index] &&
                                      touched.estiloInsumoses[index]
                                        .estilo_insumo_c_emerma &&
                                      errors.estiloInsumoses &&
                                      errors.estiloInsumoses[index] &&
                                      errors.estiloInsumoses[index][
                                        "estilo_insumo_c_emerma"
                                      ]}
                                  </>
                                }
                                onBlur={handleBlur}
                                value={
                                  estiloInsumos[index].estilo_insumo_c_emerma
                                }
                                onChange={(e) => {
                                  var _temp = estiloInsumos;
                                  _temp[index].estilo_insumo_c_emerma =
                                    e.target.value;
                                  setFieldValue("estiloInsumoses", _temp);
                                }}
                              />
                            </TableCell>

                            <TableCell>
                              <TextField
                                style={{
                                  width: 150,
                                }}
                                type="number"
                                fullWidth
                                onBlur={handleBlur}
                                value={getRowTotal(estiloInsumos[index])}
                                onChange={() => {}}
                              />
                            </TableCell>
                          </TableRow>
                        </>
                      ))}
                      <TableRow style={{ height: 30 }} hover>
                        <TableCell></TableCell>
                        <TableCell></TableCell>
                        <TableCell>TOTAL</TableCell>
                        <TableCell></TableCell>
                        <TableCell></TableCell>
                        <TableCell></TableCell>
                        <TableCell>{getTotal(values)}</TableCell>
                      </TableRow>
                    </TableBody>
                  </Table>
                </form>
              )}
            </Formik>
          </Box>
        )}
      </PerfectScrollbar>
      <Dialog
        maxWidth="md"
        fullWidth
        onClose={handleModalClose}
        open={isModalOpen}
      >
        {/* Dialog renders its body even if not open */}
        {isModalOpen && (
          <NewItem
            segments={[]}
            products={[]}
            families={[]}
            subFamilies={[]}
            familyperm={true}
            subfamilyperm={true}
            imagefile={null}
            units={units}
            _getInitialData={() => {}}
            editID={-1}
            _initialValue={[]}
            onAddComplete={handleModalClose}
            onCancel={handleModalClose}
            onDeleteComplete={handleModalClose}
            onEditComplete={handleModalClose}
          />
        )}
      </Dialog>
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
          window.setTimeout(() => {
            handleReplicateData();

            setIsReplicateModalOpen(false);
          }, 1000);
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

InsumosTable.propTypes = {
  className: PropTypes.string,
};

InsumosTable.defaultProps = {};

export default InsumosTable;
