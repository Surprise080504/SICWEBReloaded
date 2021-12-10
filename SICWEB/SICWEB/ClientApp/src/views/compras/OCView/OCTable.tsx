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
  SvgIcon,
  Tooltip,
  TablePagination,
} from "@material-ui/core";
import {
  Edit as EditIcon,
  Trash as DeleteIcon,
  Search as SearchIcon,
  Download as DownloadIcon,
  Watch as WatchIcon,
  ArrowRight as ArrowRightIcon,
  Download,
} from "react-feather";

import SearchIcon2 from "@material-ui/icons/Search";
import AddIcon2 from "@material-ui/icons/Add";

import type { Theme } from "src/theme";
import {
  getOrdenEstados,
  getOrdens,
  getOrdenDetail,
  getOrdenItems,
  getMoneda,
  download,
  deleteOrdens,
} from "src/apis/comprasApi";
import useSettings from "src/hooks/useSettings";
import ConfirmModal from "src/components/ConfirmModal";
import { useSnackbar } from "notistack";
import { useHistory } from "react-router-dom";
import { FlashAutoTwoTone } from "@material-ui/icons";
import jwtDecode from "jwt-decode";

import NewOrden from "./NewOrden";
import OrdendeCompraPDF from "../../pdf/OrdendeCompraPDF";
import OrdendeCompraMetaPDF from "../../pdf/MetaTable/OrdendeCompraMetaPDF";
import { pdf } from "@react-pdf/renderer";
import { saveAs } from "file-saver";

interface TablesProps {
  className?: string;
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

const OCTable: FC<TablesProps> = ({ className, ...rest }) => {
  const classes = useStyles();
  const { enqueueSnackbar } = useSnackbar();
  const { settings, saveSettings } = useSettings();
  const [values, setValues] = useState({
    direction: settings.direction,
    responsiveFontSizes: settings.responsiveFontSizes,
    theme: settings.theme,
  });
  const [ordens, setOrdens] = useState<any>([]);
  const [filters, setFilters] = useState({
    ruc: "",
    moneda: -1,
    estado: -1,
  });
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [deleteID, setDeleteID] = useState("-1");
  const [editID, setEditID] = useState(-1);
  const [isModalOpen2, setIsModalOpen2] = useState(false);
  const [isModalOpen3, setIsModalOpen3] = useState(false);
  const [estados, setEstados] = useState<any>([]);
  const [monedaOptions, setMonedaOptions] = useState<any>([]);
  const [page, setPage] = useState<number>(0);
  const [limit] = useState<number>(15);
  const [detailvalue, setDetailValue] = useState<any>();
  const [isEditable, setIsEditable] = useState<any>();
  const history = useHistory();

  const paginatedOrdens = applyPagination(ordens, page, limit);
  useEffect(() => {
    _getInitialData();
    _getEstados();
    _getMoneda();

    let detail = {
      id: -1,
      items: [],
      detail: {},
    };
    setDetailValue(detail);
  }, []);

  const _getEstados = () => {
    getOrdenEstados().then((res) => {
      setEstados(res);
    });
  };

  const _getMoneda = () => {
    getMoneda().then((res) => {
      setMonedaOptions(res);
    });
  };

  const _getInitialData = () => {
    handleSearch();
  };
  const handleModalClose = (): void => {
    setIsModalOpen(false);
  };

  const handleModalClose3 = (): void => {
    handleSearch();
    setIsModalOpen3(false);
  };

  const handleSearch = () => {
    getOrdens(filters)
      .then((res) => {
        setOrdens(res);
      })
      .catch((err) => {
        setOrdens([]);
      });
  };
  const handleCancel = (id) => {
    setDeleteID(id);
    setIsModalOpen2(true);
  };

  const handleMetadataDownload = async () => {
    //Frontend
    let data = [];
    await getOrdens(filters).then((res: any) => {
      data = res;
    });
    generatePdfDocument(<OrdendeCompraMetaPDF data={data} />);

    //Backend
    // download()
    //   .then((res) => {})
    //   .catch((err) => {});
  };

  const handleDownload = async (ordenID) => {
    let pdfData = {
      id: -1,
      user: "",
      items: [],
      detail: {},
    };

    pdfData.id = ordenID;

    const decoded: any = jwtDecode(localStorage.getItem("accessToken"));
    pdfData.user = decoded.unique_name;

    await getOrdenItems(ordenID).then((res: any[]) => {
      pdfData.items = res;
    });

    await getOrdenDetail(ordenID).then((res: any) => {
      pdfData.detail = res;
    });

    // generatePdfDocument(<OrdendeCompraPDF data={pdfData} />);
  };

  const generatePdfDocument = async (documentData) => {
    const blob = await pdf(documentData).toBlob();
    saveAs(blob, "ORDEN DE COMPRA.pdf");
  };

  const handleView = async (ordenID) => {
    await setEditID(ordenID);
    setIsEditable(false);

    let tmp = detailvalue;
    tmp.id = ordenID;

    await getOrdenItems(ordenID).then((res: any[]) => {
      tmp.items = res;
    });

    await getOrdenDetail(ordenID).then((res: any) => {
      tmp.detail = res;
    });
    await setDetailValue(tmp);

    await setIsModalOpen3(true);
  };

  const handleClose = (id) => {};

  const handleEdit = async (ordenID) => {
    await setEditID(ordenID);
    await setIsEditable(true);

    let tmp = detailvalue;
    tmp.id = ordenID;

    await getOrdenItems(ordenID).then((res: any[]) => {
      tmp.items = res;
    });

    await getOrdenDetail(ordenID).then((res: any) => {
      tmp.detail = res;
    });
    await setDetailValue(tmp);

    await setIsModalOpen3(true);
  };

  const handleCreate = async () => {
    await setEditID(-1);
    await setIsEditable(true);

    let detail = {
      id: -1,
      items: [],
      detail: {},
    };

    await setDetailValue(detail);

    await setIsModalOpen3(true);
  };

  const handlePageChange = (event: any, newPage: number): void => {
    setPage(newPage);
  };

  return (
    <Card className={clsx(classes.root, className)} {...rest}>
      <Box p={3} alignItems="center">
        <Grid container spacing={3}>
          <Grid item lg={6} sm={6} xs={12}>
            <Grid container spacing={3}>
              <Grid item lg={4} sm={6} xs={12}>
                <TextField
                  fullWidth
                  size="small"
                  label="RUC Prov."
                  placeholder="RUC Prov."
                  variant="outlined"
                  value={filters.ruc}
                  onChange={(e) =>
                    setFilters({ ...filters, ruc: e.target.value })
                  }
                />
              </Grid>
              <Grid item lg={4} sm={4} xs={12}>
                <TextField
                  fullWidth
                  size="small"
                  label="Moneda"
                  placeholder=""
                  variant="outlined"
                  value={filters.moneda}
                  SelectProps={{ native: true }}
                  select
                  onChange={(e) => {
                    setFilters({
                      ...filters,
                      moneda: Number(e.target.value),
                    });
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
              <Grid item lg={4} sm={4} xs={12}>
                <TextField
                  fullWidth
                  size="small"
                  label="Estado"
                  placeholder=""
                  variant="outlined"
                  value={filters.estado}
                  SelectProps={{ native: true }}
                  select
                  onChange={(e) => {
                    _getEstados();
                    setFilters({
                      ...filters,
                      estado: Number(e.target.value),
                    });
                  }}
                >
                  <option key="-1" value="-1">
                    {"-- Seleccionar --"}
                  </option>
                  {estados.map((estadooption) => (
                    <option
                      key={estadooption.odc_estado_iid}
                      value={estadooption.odc_estado_iid}
                    >
                      {estadooption.odc_estado_vdescripcion}
                    </option>
                  ))}
                </TextField>
              </Grid>
            </Grid>
          </Grid>
          <Grid item lg={2} sm={2} xs={12}>
            <></>
          </Grid>
          <Grid item lg={4} sm={3} xs={12}>
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
                <Button
                  variant="contained"
                  color="secondary"
                  startIcon={<AddIcon2 />}
                  onClick={() => handleCreate()}
                >
                  {"Nuevo"}
                </Button>
              </Grid>

              <Grid item>
                <Button
                  variant="contained"
                  color="secondary"
                  startIcon={<Download />}
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
                <TableCell>SERIE</TableCell>
                <TableCell>CÓDIGO</TableCell>
                <TableCell>RUC PROVEEDOR</TableCell>
                <TableCell>PROVEEDOR</TableCell>
                <TableCell>ESTADO</TableCell>
                <TableCell>MONEDA</TableCell>
                <TableCell>MONTO TOTAL</TableCell>
                <TableCell align="right">&nbsp;</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {paginatedOrdens.map((item, index) => {
                return (
                  <TableRow style={{ height: 30 }} hover key={index}>
                    <TableCell>{item.serie}</TableCell>
                    <TableCell>{item.codigo}</TableCell>
                    <TableCell>{item.ruc}</TableCell>
                    <TableCell>{item.prov}</TableCell>
                    <TableCell>{item.estado}</TableCell>
                    <TableCell>{item.moneda}</TableCell>
                    <TableCell>{item.monototal}</TableCell>
                    <TableCell align="right">
                      <Tooltip title="Ver" aria-label="Ver">
                        <IconButton onClick={() => handleView(item.id)}>
                          <SvgIcon fontSize="small">
                            <WatchIcon />
                          </SvgIcon>
                        </IconButton>
                      </Tooltip>
                      <Tooltip title="Modificar" aria-label="Modificar">
                        <IconButton onClick={() => handleEdit(item.id)}>
                          <SvgIcon fontSize="small">
                            <EditIcon />
                          </SvgIcon>
                        </IconButton>
                      </Tooltip>
                      <Tooltip title="Anular" aria-label="Anular">
                        <IconButton onClick={() => handleCancel(item.id)}>
                          <SvgIcon fontSize="small">
                            <DeleteIcon />
                          </SvgIcon>
                        </IconButton>
                      </Tooltip>
                      <Tooltip title="Cerrar" aria-label="Cerrar">
                        <IconButton onClick={() => handleClose(item.id)}>
                          <SvgIcon fontSize="small">
                            <ArrowRightIcon />
                          </SvgIcon>
                        </IconButton>
                      </Tooltip>
                      <Tooltip title="Descargar" aria-label="Descargar">
                        <IconButton onClick={() => handleDownload(item.id)}>
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
            count={ordens.length}
            onPageChange={handlePageChange}
            onRowsPerPageChange={() => {}}
            page={page}
            rowsPerPage={limit}
            rowsPerPageOptions={[15]}
          />
        </Box>
      </PerfectScrollbar>
      <Dialog
        maxWidth="xl"
        fullWidth
        onClose={handleModalClose3}
        open={isModalOpen3}
      >
        {isModalOpen3 && (
          <NewOrden
            editID={detailvalue.id}
            initialValue={detailvalue.detail}
            initialItem={detailvalue.items}
            editable={!isEditable}
            handleSearch={handleSearch}
            onCancel={handleModalClose3}
          />
        )}
      </Dialog>
      <ConfirmModal
        open={isModalOpen2}
        title={"¿Está seguro de eliminar este Orden?"}
        setOpen={() => setIsModalOpen2(false)}
        onConfirm={() => {
          saveSettings({ saving: true });
          window.setTimeout(() => {
            deleteOrdens(deleteID)
              .then((res) => {
                if (!res) {
                  setIsModalOpen2(false);
                  handleSearch();
                  enqueueSnackbar(
                    "El item está siendo usado por un Estilo, no se puede eliminar",
                    {
                      variant: "error",
                    }
                  );
                  saveSettings({ saving: false });
                } else {
                  saveSettings({ saving: false });
                  _getInitialData();
                  enqueueSnackbar("Tus datos se han guardado exitosamente.", {
                    variant: "success",
                  });

                  setIsModalOpen2(false);
                  handleSearch();
                }
              })
              .catch((err) => {
                setIsModalOpen2(false);
                handleSearch();
                enqueueSnackbar("No se pudo guardar.", {
                  variant: "error",
                });
                saveSettings({ saving: false });
              });
          }, 1000);
        }}
      />
    </Card>
  );
};

OCTable.propTypes = {
  className: PropTypes.string,
};

OCTable.defaultProps = {};

export default OCTable;
