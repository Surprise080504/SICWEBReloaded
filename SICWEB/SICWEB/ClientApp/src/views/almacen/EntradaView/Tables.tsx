import { useEffect, useState, FC, useRef } from "react";
import { useSelector } from "react-redux";
import { useSnackbar } from "notistack";
import clsx from "clsx";
import PropTypes from "prop-types";
import PerfectScrollbar from "react-perfect-scrollbar";
import { useReactToPrint } from 'react-to-print';
import {
  Box,
  Button,
  Card,
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
  SvgIcon,
  Tooltip,
  TablePagination,
} from "@material-ui/core";
import Backdrop from '@material-ui/core/Backdrop';
import CircularProgress from '@material-ui/core/CircularProgress';
import {
  Edit as EditIcon,
  Download as DownloadIcon,
  Watch as WatchIcon,
} from "react-feather";
import SearchIcon2 from "@material-ui/icons/Search";
import AddIcon2 from "@material-ui/icons/Add";
import HighlightOffIcon from '@material-ui/icons/HighlightOff';
import BlockIcon from '@material-ui/icons/Block';
import { pdf } from "@react-pdf/renderer";
import { saveAs } from "file-saver";
import { format } from 'date-fns';
import type { Theme } from "src/theme";
import {
  getEntradas,
  getEstados,
  saveEntrada,
  changeToCerrar,
  changeToAnular
} from "src/apis/entradaApi";
import ConfirmModal from "src/components/ConfirmModal";
import { useHistory } from "react-router-dom";
import { getPermission } from "src/apis/userApi";
import _ from "lodash";
import NewEntrada from "./NewEntrada";
import NewEntradaPDF from "./NewEntradaPDF";
import EntradaMetaPDF from "src/views/pdf/EntradaTable";

interface TablesProps {
  className?: string;
}

interface Filters {
  ruc: string,
  desde: string,
  hasta: string,
  razonsocial: string,
  estado: number
}

const applyPagination = (
  entradas: any,
  page: number,
  limit: number
): any[] => {
  return entradas.slice(page * limit, page * limit + limit);
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
  backdrop: {
    zIndex: theme.zIndex.drawer + 1,
    color: '#fff',
  }
}));

const Tables: FC<TablesProps> = ({ className, ...rest }) => {
  const classes = useStyles();

  const { enqueueSnackbar } = useSnackbar();
  const [estados, setEstados] = useState<any>([]);
  const [entradas, setEntradas] = useState<any>([]);

  const [filters, setFilters] = useState<Filters>({
    ruc: '',
    desde: '',
    hasta: '',
    razonsocial: '',
    estado: -1
  });

  const [loading, setLoading] = useState(false);
  const [mode, setMode] = useState<boolean>(false);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [aopen, setAopen] = useState(false);
  const [copen, setCopen] = useState(false);
  const [currentID, setCurrenctID] = useState<number>(-1);
  const [page, setPage] = useState<number>(0);
  const [limit] = useState<number>(15);

  const paginatedItems = applyPagination(entradas, page, limit);

  const history = useHistory();

  const [permission, setPermission] = useState<any>([]);
  const { menuPermission } = useSelector((state: any) => state.businesses);

  useEffect(() => {
    _getInitialData();
    const pathname = history.location?.pathname ?? "";
    const menuItem = getPermission(menuPermission, pathname);
    if (_.isEmpty(menuItem)) {
      history.push("/404");
    }
    setPermission(menuItem);
  }, []);

  const _getEstados = () => {
    getEstados().then((res) => {
      setEstados(res);
    }).catch((err) => {
      setEstados([]);
    });
  };

  const handleSearch = () => {
    getEntradas(filters)
      .then((res) => {
        setEntradas(res);
        console.log(res);
      })
      .catch((err) => {
        setEntradas([]);
      });
  };

  const _getInitialData = async () => {
    await setLoading(true);
    await _getEstados();
    await handleSearch();
    await setLoading(false);
  };

  const handleModalClose = (): void => {
    setIsModalOpen(false);
  };


  const handleEdit = (id) => {
    setMode(false);
    setCurrenctID(id);
    setIsModalOpen(true);
  };

  const handleView = (id) => {
    setMode(true);
    setCurrenctID(id);
    setIsModalOpen(true);
  };

  const handlePageChange = (event: any, newPage: number): void => {
    setPage(newPage);
  };

  const successSave = async (saveData: any) => {
    await setLoading(true);
    await handleSearch();
    await handleModalClose();
    await setLoading(false);
    if (_checkClose(saveData))
      enqueueSnackbar("Insertado con éxito. El movimiento se encuentra CERRADO debido a que se ingresaron todos los ítems de orden de compra Además, la orden de compra relacionada, ha sido CERRADA.", {
        variant: "success",
      });
    else enqueueSnackbar("Tus datos se han guardado exitosamente.", {
      variant: "success",
    });
  }

  const _checkZero = (data: any) => {
    let count = 0;
    data.items.forEach(item => {
      if (Number(item.recibida) === 0) {
        count++;
      }
    })
    if (count === data.items.length) return false;
    else return true;
  }

  const _checkClose = (data: any) => {
    let count = 0;
    data.items.forEach(item => {
      if ((Number(item.recibida) + Number(item.atendida)) >= Number(item.pedida)) {
        count++;
      }
    })
    if (count === data.items.length) return true;
    else return false;
  }

  const _saveEntrada = (saveData) => {
    if (!_checkZero(saveData)) {
      enqueueSnackbar("Debe ingresar al menos un item al movimiento.", {
        variant: "warning",
      });
      return;
    }
    let flag: boolean = true;
    let descripcion: string = "";
    saveData.items.forEach(item => {
      if ((Number(item.recibida) + Number(item.atendida)) > Number(item.maxatendida)) {
        flag = false;
        descripcion = item.descripcion;
        return;
      }
    })
    if (flag)
      saveEntrada(saveData)
        .then((res: any) => {
          if (res > -1)
            successSave(saveData);
        })
        .catch((err) => {
          enqueueSnackbar("No se pudo guardar.", {
            variant: "error",
          });
        });
    else enqueueSnackbar(`se esta ingresando una cantidad en exceso del item ${descripcion}`, {
      variant: "warning",
    });
  }

  const successChange = async () => {
    await setLoading(true);
    await handleSearch();
    await setLoading(false);
    await enqueueSnackbar("El estado ha cambiado exitosamente.", {
      variant: "success",
    });
  }

  const _changeToCerrar = () => {
    changeToCerrar(currentID)
      .then((res: any) => {
        if (res > -1)
          successChange();
      })
      .catch((err) => {
        enqueueSnackbar("El estado no puede cambiar.", {
          variant: "error",
        });
      });
  }

  const _changeToAnular = () => {
    changeToAnular(currentID)
      .then((res: any) => {
        if (res > -1)
          successChange();
      })
      .catch((err) => {
        enqueueSnackbar("El estado no puede cambiar.", {
          variant: "error",
        });
      });
  }

  const generatePdfDocument = async (documentData) => {
    const blob = await pdf(documentData).toBlob();
    saveAs(blob, "Movimiento De Entradas.pdf");
  };

  const handleMetadataDownload = async () => {
    let data = [];
    await getEntradas(filters).then((res: any) => {
      data = res;
    });
    generatePdfDocument(<EntradaMetaPDF data={data} />);
  };

  const componentRef = useRef();

  const handlePrint = useReactToPrint({
    content: () => componentRef.current,
  });

  return (
    <Card className={clsx(classes.root, className)} {...rest}>
      <Box p={3} alignItems="center">
        <Grid container spacing={3}>
          <Grid item xs={6}>
            <Grid container spacing={1}>
              <Grid item lg={6} sm={6} xs={12}>
                <TextField
                  fullWidth
                  size="small"
                  label="RUC"
                  placeholder=""
                  variant="outlined"
                  value={filters.ruc}
                  onChange={(e) =>
                    setFilters({ ...filters, ruc: e.target.value })
                  }
                />
              </Grid>
              <Grid item lg={6} sm={6} xs={12}>
                <TextField
                  fullWidth
                  size="small"
                  label="Razón Social"
                  placeholder=""
                  variant="outlined"
                  value={filters.razonsocial}
                  onChange={(e) =>
                    setFilters({ ...filters, razonsocial: e.target.value })
                  }
                />
              </Grid>
              <Grid item lg={4} sm={4} xs={12}>
                <TextField
                  id="date"
                  label="Desde"
                  size="small"
                  type="date"
                  variant="outlined"
                  value={filters.desde}
                  InputLabelProps={{
                    shrink: true,
                  }}
                  fullWidth
                  onChange={(e) =>
                    setFilters({ ...filters, desde: e.target.value })
                  }
                />
              </Grid>
              <Grid item lg={4} sm={4} xs={12}>
                <TextField
                  id="date"
                  label="Hasta"
                  type="date"
                  size="small"
                  variant="outlined"
                  value={filters.hasta}
                  InputLabelProps={{
                    shrink: true,
                  }}
                  fullWidth
                  onChange={(e) =>
                    setFilters({ ...filters, hasta: e.target.value })
                  }
                />
              </Grid>
              <Grid item lg={4} sm={4} xs={12}>
                <TextField
                  fullWidth
                  size="small"
                  label="Estado"
                  select
                  SelectProps={{ native: true }}
                  variant="outlined"
                  value={filters.estado || -1}
                  onChange={(e) => {
                    setFilters({
                      ...filters,
                      estado: Number(e.target.value),
                    });
                  }}
                >
                  <option key="-1" value="-1">
                    {"-- Seleccionar --"}
                  </option>
                  {estados.map((estado) => (
                    <option key={estado.mov_estado_iid} value={estado.mov_estado_iid}>
                      {estado.mov_estado_vdescrpcion}
                    </option>
                  ))}
                </TextField>
              </Grid>
            </Grid>
          </Grid>
          <Grid item xs={3}>
            <></>
          </Grid>
          <Grid item xs={3}>
            <Grid container spacing={3}>
              <Grid item>
                <Button
                  onClick={handleSearch}
                  variant="contained"
                  color="primary"
                  startIcon={<SearchIcon2 />}
                >
                  {"Buscar"}
                </Button>
              </Grid>
              <Grid item>
                {permission?.perf_menu_c_calta == "A" && (
                  <Button
                    variant="contained"
                    color="secondary"
                    startIcon={<AddIcon2 />}
                    onClick={() => handleEdit(-1)}
                  >
                    {"Nuevo"}
                  </Button>
                )}
              </Grid>
              <Grid item>
                <Button
                  variant="contained"
                  color="default"
                  startIcon={<DownloadIcon />}
                  onClick={handleMetadataDownload}
                >
                  {"Descargar"}
                </Button>
              </Grid>
            </Grid>
          </Grid>
        </Grid>

      </Box>
      <PerfectScrollbar>
        <Box minWidth={1200}>
          <Table stickyHeader>
            <TableHead style={{ background: "red" }}>
              <TableRow>
                <TableCell>MOV. ID</TableCell>
                <TableCell>ODC. SERIE</TableCell>
                <TableCell>ODC. CÓDIGO</TableCell>
                <TableCell>RUC PROVEEDOR</TableCell>
                <TableCell>RAZÓN SOCIAL PROVEEDOR</TableCell>
                <TableCell>FECHA REGISTRO</TableCell>
                <TableCell>ESTADO</TableCell>
                <TableCell align="right">&nbsp;</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {paginatedItems.map((item, index) => {
                return (
                  <TableRow style={{ height: 30 }} hover key={index}>
                    <TableCell>{item.id}</TableCell>
                    <TableCell>{item.odc_c_cserie}</TableCell>
                    <TableCell>{item.odc_c_vcodigo}</TableCell>
                    <TableCell>{item.ruc}</TableCell>
                    <TableCell>{item.razonsocial}</TableCell>
                    <TableCell>{format(new Date(item.fecha), 'dd/MM/yyyy HH:mm')}</TableCell>
                    <TableCell>{item.estado}</TableCell>
                    <TableCell align="right">
                      <Tooltip title="Ver" aria-label="Ver">
                        <IconButton onClick={() => handleView(item.id)}>
                          <SvgIcon fontSize="small">
                            <WatchIcon />
                          </SvgIcon>
                        </IconButton>
                      </Tooltip>
                      <Tooltip title="Modificar" aria-label="Modificar">
                        <IconButton onClick={() => {
                          if (item.estado_id === 2)
                            handleEdit(item.id)
                          else enqueueSnackbar("No se pueden modificar movimiento en estado CERRADO o ANULADO.", {
                            variant: "warning",
                          });
                        }}>
                          <SvgIcon fontSize="small">
                            <EditIcon />
                          </SvgIcon>
                        </IconButton>
                      </Tooltip>
                      <Tooltip title="Cerrar" aria-label="Cerrar">
                        <IconButton onClick={() => {
                          if (item.estado_id === 2) {
                            setCopen(true);
                            setCurrenctID(item.id);
                          }
                          else if (item.estado_id === 3)
                            enqueueSnackbar("El Movimiento ya se encuentra en estado CERRADO.", {
                              variant: "warning",
                            });
                          else enqueueSnackbar("No se puede CERRAR movimientos en estado ANULADO.", {
                            variant: "warning",
                          });
                        }}>
                          <SvgIcon fontSize="small">
                            <HighlightOffIcon />
                          </SvgIcon>
                        </IconButton>
                      </Tooltip>
                      <Tooltip title="Anular" aria-label="Anular">
                        <IconButton onClick={() => {
                          if (item.estado_id === 2) {
                            setAopen(true);
                            setCurrenctID(item.id);
                          }
                          else if (item.estado_id === 3)
                            enqueueSnackbar("No se puede CERRAR movimientos en estado ANULADO.", {
                              variant: "warning",
                            });
                          else enqueueSnackbar("El Movimiento ya se encuentra en estado ANULADO.", {
                            variant: "warning",
                          });
                        }}>
                          <SvgIcon fontSize="small">
                            <BlockIcon />
                          </SvgIcon>
                        </IconButton>
                      </Tooltip>
                      <Tooltip title="Descargar" aria-label="Descargar">
                        <IconButton onClick={async () => {
                          await setCurrenctID(item.id);
                          await handlePrint();
                        }}>
                          <SvgIcon fontSize="small">
                            <DownloadIcon />
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
            count={entradas.length}
            onPageChange={handlePageChange}
            onRowsPerPageChange={() => { }}
            page={page}
            rowsPerPage={limit}
            rowsPerPageOptions={[15]}
          />
        </Box>
      </PerfectScrollbar>

      <Dialog
        maxWidth="xl"
        fullWidth
        onClose={handleModalClose}
        open={isModalOpen}
      >
        {/* Dialog renders its body even if not open */}
        {isModalOpen && (
          <NewEntrada
            mode={mode}
            currentID={currentID}
            closeModal={handleModalClose}
            saveEntrada={_saveEntrada}
          />
        )}
      </Dialog>
      <ConfirmModal
        title="SICWEB - INDUMET"
        open={aopen}
        setOpen={setAopen}
        onConfirm={_changeToAnular}
      >
        <p>¿Está seguro de Anular el Movimiento seleccionado?</p>
      </ConfirmModal>
      <ConfirmModal
        title="SICWEB - INDUMET"
        open={copen}
        setOpen={setCopen}
        onConfirm={_changeToCerrar}
      >
        <p>¿Está seguro de 	Cerrar el Movimiento seleccionado?</p>
      </ConfirmModal>
      <Backdrop className={classes.backdrop} open={loading}>
        <CircularProgress color="inherit" />
      </Backdrop>
      <div
        style={{ display: "none" }}// This make NewEntradaPDF show only while printing
      >
        <NewEntradaPDF
          currentID={currentID}
          saveEntrada={_saveEntrada}
          customRef={componentRef}
        />
      </div>
    </Card >
  );
};

Tables.propTypes = {
  className: PropTypes.string
};

export default Tables;