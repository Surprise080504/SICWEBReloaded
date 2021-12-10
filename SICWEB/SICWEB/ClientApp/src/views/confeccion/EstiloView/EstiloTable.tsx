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
} from "react-feather";

import SearchIcon2 from "@material-ui/icons/Search";
import AddIcon2 from "@material-ui/icons/Add";

import type { Theme } from "src/theme";
import useSettings from "src/hooks/useSettings";
import ConfirmModal from "src/components/ConfirmModal";
import { useSnackbar } from "notistack";
import NewEstilo from "./NewEstilo";

import { deleteStyle, getStyle } from "src/apis/styleApi";
import { useHistory } from "react-router-dom";
import { useSelector } from "react-redux";
import _ from "lodash";
import { getPermission } from "src/apis/userApi";
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
const EstiloTable: FC<TablesProps> = ({ className, ...rest }) => {
  const classes = useStyles();
  const { enqueueSnackbar } = useSnackbar();
  const { settings, saveSettings } = useSettings();
  const history = useHistory();
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
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [deleteID, setDeleteID] = useState("-1");
  const [editID, setEditID] = useState(-1);
  const [isModalOpen2, setIsModalOpen2] = useState(false);
  const [page, setPage] = useState<number>(0);
  const [limit] = useState<number>(15);
  const paginatedStyles = applyPagination(styles, page, limit);
  const [permission, setPermission] = useState<any>([]);
  const menuPermission = useSelector(
    (state: any) => state?.businesses?.menuPermission ?? []
  );
  useEffect(() => {
    _getInitialData();
    const pathname = history.location?.pathname ?? "";
    const menuItem = getPermission(menuPermission, pathname);
    if (_.isEmpty(menuItem)) {
      history.push("/404");
    }
    setPermission(menuItem);
  }, []);
  const _getInitialData = () => {
    handleSearch();
  };
  const handleModalClose = (): void => {
    setIsModalOpen(false);
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

  const handleProcess = (id, item) => {
    history.push("/Interfaces/confeccion/process/" + id, item);
  };

  const handleInsumos = (id, item) => {
    history.push("/Interfaces/confeccion/insumos/" + id, item);
  };

  const handleDelete = (id) => {
    setDeleteID(id);
    setIsModalOpen2(true);
  };
  const handleEdit = (id) => {
    setEditID(id);
    setIsModalOpen(true);
  };
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
                  label="Código Estilo"
                  placeholder="Código Estilo"
                  variant="outlined"
                  value={filters.code}
                  onChange={(e) =>
                    setFilters({ ...filters, code: e.target.value })
                  }
                />
              </Grid>
              <Grid item lg={4} sm={6} xs={12}>
                <TextField
                  fullWidth
                  size="small"
                  label="Nombre Estilo"
                  placeholder="Nombre Estilo"
                  variant="outlined"
                  value={filters.name}
                  onChange={(e) =>
                    setFilters({ ...filters, name: e.target.value })
                  }
                />
              </Grid>
              <Grid item lg={4} sm={6} xs={12}>
                <TextField
                  fullWidth
                  size="small"
                  label="Color Estilo"
                  placeholder="Color Estilo"
                  variant="outlined"
                  value={filters.color}
                  onChange={(e) =>
                    setFilters({ ...filters, color: e.target.value })
                  }
                />
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
              {/* <Grid item>
                <Button variant="contained" color="secondary" startIcon={<AddIcon2 />} onClick={() => handleEdit(-1)}>{'Nuevo'}</Button>
              </Grid> */}
            </Grid>
          </Grid>
        </Grid>
      </Box>
      <PerfectScrollbar>
        <Box minWidth={1200}>
          <Table stickyHeader>
            <TableHead style={{ background: "red" }}>
              <TableRow>
                <TableCell>Código</TableCell>
                <TableCell>Nombre</TableCell>
                <TableCell>Descripción</TableCell>
                <TableCell>Item</TableCell>
                <TableCell>Marca</TableCell>
                <TableCell>Categoría de Marca</TableCell>
                <TableCell>Color de Marca</TableCell>
                <TableCell>Talla</TableCell>
                <TableCell align="right">&nbsp;</TableCell>
                <TableCell align="right">&nbsp;</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {paginatedStyles.map((item, index) => {
                return (
                  <TableRow
                    style={{ height: 30 }}
                    hover
                    // key={item.estilo_c_iid}
                    key={index.toString()}
                  >
                    <TableCell>{item.estilo_c_vcodigo}</TableCell>
                    <TableCell>{item.estilo_c_vnombre}</TableCell>
                    <TableCell>{item.estilo_c_vdescripcion}</TableCell>
                    <TableCell>{item.itemName}</TableCell>
                    <TableCell>{item.brandName}</TableCell>
                    <TableCell>{item.categoryName}</TableCell>
                    <TableCell>{item.colorName}</TableCell>
                    <TableCell>{item.estilo_talla_c_vid}</TableCell>
                    <TableCell align="right">
                      {permission?.perf_menu_c_cmod == "A" && (
                        <Tooltip title="Editar" aria-label="Editar">
                          <Button
                            size="small"
                            onClick={() =>
                              handleProcess(
                                item.estilo_c_iid,
                                //item.estilo_talla_c_iid,
                                item
                              )
                            }
                            color="primary"
                          >
                            {"Procesos"}
                          </Button>
                        </Tooltip>
                      )}
                    </TableCell>
                    <TableCell align="right">
                      {permission?.perf_menu_c_cmod == "A" && (
                        <Tooltip title="Editar" aria-label="Editar">
                          <Button
                            size="small"
                            onClick={() =>
                              handleInsumos(item.estilo_c_iid, item)
                            }
                            color="primary"
                          >
                            {"Insumos"}
                          </Button>
                        </Tooltip>
                      )}
                    </TableCell>
                  </TableRow>
                );
              })}
            </TableBody>
          </Table>
          <TablePagination
            component="div"
            count={styles.length}
            onPageChange={handlePageChange}
            onRowsPerPageChange={() => {}}
            page={page}
            rowsPerPage={limit}
            rowsPerPageOptions={[15]}
          />
        </Box>
      </PerfectScrollbar>
      <Dialog
        maxWidth="md"
        fullWidth
        onClose={handleModalClose}
        open={isModalOpen}
      >
        {isModalOpen && (
          <NewEstilo
            editID={editID}
            _initialValue={styles}
            handleSearch={handleSearch}
            onCancel={handleModalClose}
          />
        )}
      </Dialog>
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

EstiloTable.propTypes = {
  className: PropTypes.string,
};

EstiloTable.defaultProps = {};

export default EstiloTable;
