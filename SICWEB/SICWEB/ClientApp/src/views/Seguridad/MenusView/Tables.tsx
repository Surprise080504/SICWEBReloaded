import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
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
import type { Product } from "src/types/product";
import NewMenu from "./NewMenu";
import OPCView from "./OPCView";
import { getParentMenus, getMenu, deleteMenu } from "src/apis/menuApi";
import useSettings from "src/hooks/useSettings";
import ConfirmModal from "src/components/ConfirmModal";
import { useSnackbar } from "notistack";
import { Pages } from "@material-ui/icons";
import { useHistory } from "react-router-dom";
import { getPermission } from "src/apis/userApi";
import _ from "lodash";

interface TablesProps {
  className?: string;
  products: Product[];
}

interface Filters {
  availability?: "available" | "unavailable";
  category?: string;
  inStock?: boolean;
  isShippable?: boolean;
}

const sortOptions = [
  {
    value: "updatedAt|desc",
    label: "Last update (newest first)",
  },
  {
    value: "updatedAt|asc",
    label: "Last update (oldest first)",
  },
  {
    value: "createdAt|desc",
    label: "Creation date (newest first)",
  },
  {
    value: "createdAt|asc",
    label: "Creation date (oldest first)",
  },
];

const estadoOptions = [
  {
    value: 1,
    label: "Activo",
  },
  {
    value: 0,
    label: "Inactivo",
  },
];

const applyPagination = (
  products: any[],
  page: number,
  limit: number
): any[] => {
  return products.slice(page * limit, page * limit + limit);
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

const Tables: FC<TablesProps> = ({ className, ...rest }) => {
  const classes = useStyles();
  const { enqueueSnackbar } = useSnackbar();
  const { settings, saveSettings } = useSettings();

  const [values, setValues] = useState({
    direction: settings.direction,
    responsiveFontSizes: settings.responsiveFontSizes,
    theme: settings.theme,
  });
  const [menu, setMenu] = useState<any>([]);
  const [parent_menu, setParent_menu] = useState<any>([]);
  const [parent_id, setParent_id] = useState<any>([]);
  const [nivel, setNivel] = useState<any>([]);
  const [pagina, setPagina] = useState<any>([]);
  const [opciones, setOpciones] = useState<any>([]);
  const [estado, setEstado] = useState<any>([]);

  const [items, setItems] = useState<any>([]);

  const [filters, setFilters] = useState({
    menu: "",
    parent_id: -1,
    state: -1,
  });

  const [isModalOpen, setIsModalOpen] = useState(false);

  const [deleteID, setDeleteID] = useState("-1");
  const [editID, setEditID] = useState(-1);

  const [isModalOpen2, setIsModalOpen2] = useState(false);

  const [isModalOpen3, setIsModalOpen3] = useState(false);

  const [page, setPage] = useState<number>(0);
  const [limit] = useState<number>(15);

  const paginatedItems = applyPagination(items, page, limit);

  const { menuPermission } = useSelector((state: any) => state.businesses);
  const history = useHistory();

  const [permission, setPermission] = useState<any>([]);

  const [parentMenus, setParentMenus] = useState<any>([]);

  const _getParentMenus = () => {
    getParentMenus().then((res) => {
      setParentMenus(res);
    });
  };

  useEffect(() => {
    _getInitialData();
    _getParentMenus();
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
    setIsModalOpen3(false);
  };

  const handleSearch = () => {
    setPage(0);
    getMenu(filters)
      .then((res) => {
        setItems(res);
      })
      .catch((err) => {
        setItems([]);
      });
  };

  const handleDelete = (id) => {
    setDeleteID(id);
    setIsModalOpen2(true);
  };

  const handleEdit = (id) => {
    setEditID(id);
    setIsModalOpen(true);
  };

  const handleView = (id) => {
    setEditID(id);
    setIsModalOpen3(true);
  };

  const handlePageChange = (event: any, newPage: number): void => {
    setPage(newPage);
  };

  var keys = [0, 1, 2];
  var nivelvalues = ["", "Menú", "Pantalla"];
  const nivelOptions = {};
  for (var i = 0; i < keys.length; i++) {
    nivelOptions[keys[i]] = nivelvalues[i];
  }

  return (
    <Card className={clsx(classes.root, className)} {...rest}>
      <Box p={3} alignItems="center">
        <Grid container spacing={3}>
          <Grid item lg={8} sm={6} xs={12}>
            <Grid container spacing={3}>
              <Grid item lg={5} sm={5} xs={12}>
                <TextField
                  fullWidth
                  size="small"
                  label="Menú"
                  placeholder=""
                  variant="outlined"
                  value={filters.menu}
                  onChange={(e) =>
                    setFilters({ ...filters, menu: e.target.value })
                  }
                />
              </Grid>
              <Grid item lg={4} sm={4} xs={12}>
                <TextField
                  fullWidth
                  size="small"
                  label="Menú padre"
                  placeholder=""
                  variant="outlined"
                  value={filters.parent_id}
                  SelectProps={{ native: true }}
                  select
                  onChange={(e) => {
                    _getParentMenus();
                    setFilters({
                      ...filters,
                      parent_id: Number(e.target.value),
                    });
                  }}
                >
                  {<option key="-1" value="-1"></option>}
                  {parentMenus.map((parentMenu) => (
                    <option
                      key={parentMenu.menu_c_iid}
                      value={parentMenu.menu_c_iid}
                    >
                      {parentMenu.menu_c_vnomb}
                    </option>
                  ))}
                </TextField>
              </Grid>
              <Grid item lg={3} sm={3} xs={12}>
                <TextField
                  size="small"
                  fullWidth
                  SelectProps={{ native: true }}
                  select
                  label={<label>Estado</label>}
                  onChange={(e) =>
                    setFilters({ ...filters, state: Number(e.target.value) })
                  }
                  value={filters.state}
                  variant="outlined"
                  InputLabelProps={{
                    shrink: true,
                  }}
                >
                  {estadoOptions.map((option) => (
                    <option key={option.value} value={option.value}>
                      {option.label}
                    </option>
                  ))}
                </TextField>
              </Grid>
            </Grid>
          </Grid>
          <Grid item lg={2} sm={3} xs={12}></Grid>
          <Grid item lg={2} sm={6} xs={12}>
            <Grid container spacing={3} justifyContent="flex-end">
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
                    onClick={() => handleEdit("-1")}
                  >
                    {"Nuevo"}
                  </Button>
                )}
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
                <TableCell>Menú</TableCell>
                <TableCell>Menú padre</TableCell>
                <TableCell>Nivel</TableCell>
                <TableCell>Página</TableCell>
                <TableCell>Opciones</TableCell>
                <TableCell>Estado</TableCell>
                <TableCell align="right">&nbsp;</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {paginatedItems.map((item, index) => {
                var vEstado = "";
                if (item.estado == 1) {
                  vEstado = "Activo";
                } else if (item.estado == 0) {
                  vEstado = "Inactivo";
                } else {
                  vEstado = "";
                }
                return (
                  <TableRow style={{ height: 30 }} hover key={item.menu_c_iid}>
                    <TableCell>{item.menu_c_vnomb}</TableCell>
                    <TableCell>{item.parent_menu_c_vnomb}</TableCell>
                    <TableCell>{nivelOptions[item.menu_c_ynivel]}</TableCell>
                    <TableCell>
                      {item.menu_c_ynivel == 1 ? "" : item.menu_c_vpag_asp}
                    </TableCell>
                    <TableCell>
                      {item.opciones > 0 && (
                        <Tooltip title="Ver" aria-label="Ver">
                          <IconButton
                            onClick={() => handleView(item.menu_c_iid)}>
                            <SvgIcon fontSize="small">
                              <SearchIcon />
                            </SvgIcon>
                          </IconButton>
                        </Tooltip>
                      )}
                    </TableCell>
                    <TableCell>{vEstado}</TableCell>
                    <TableCell align="right">
                      {permission?.perf_menu_c_cmod == "A" && (
                        <Tooltip title="Editar" aria-label="Editar">
                          <IconButton
                            onClick={() => handleEdit(page * limit + index)}
                          >
                            <SvgIcon fontSize="small">
                              <EditIcon />
                            </SvgIcon>
                          </IconButton>
                        </Tooltip>
                      )}
                      {permission?.perf_menu_c_cvisual == "A" && (
                        <Tooltip title="Eliminar" aria-label="Eliminar">
                          <IconButton
                            onClick={() => handleDelete(item.menu_c_iid)}
                          >
                            <SvgIcon fontSize="small">
                              <DeleteIcon />
                            </SvgIcon>
                          </IconButton>
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
            count={items.length}
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
        {/* Dialog renders its body even if not open */}
        {isModalOpen && (
          <NewMenu
            parent_id={parent_id}
            menu={menu}
            parent_menu={parent_menu}
            nivel={nivel}
            pagina={pagina}
            opciones={opciones}
            estado={estado}
            profileid={permission?.perf_c_yid}
            _getInitialData={_getInitialData}
            editID={editID}
            _initialValue={items}
            onAddComplete={handleModalClose}
            onCancel={handleModalClose}
            onDeleteComplete={handleModalClose}
            onEditComplete={handleModalClose}
          />
        )}
      </Dialog>
      <Dialog
        maxWidth="md"
        fullWidth
        onClose={handleModalClose}
        open={isModalOpen3}
      >
        {/* Dialog renders its body even if not open */}
        {isModalOpen3 && (
          <OPCView
            editID={editID}
            onAddComplete={handleModalClose}
            onCancel={handleModalClose}
            onDeleteComplete={handleModalClose}
            onEditComplete={handleModalClose}
          />
        )}
      </Dialog>
      <ConfirmModal
        open={isModalOpen2}
        title={"¿Está seguro de eliminar este Menú o Pantalla?"}
        setOpen={() => setIsModalOpen2(false)}
        onConfirm={() => {
          saveSettings({ saving: true });
          window.setTimeout(() => {
            deleteMenu(deleteID)
              .then((res) => {
                saveSettings({ saving: false });
                _getInitialData();
                if (res === 2) {
                  //have parent menu
                  enqueueSnackbar("Este Menú sirve de base para pantallas", {
                    variant: "error",
                  });
                } else if (res === 1) {
                  //have profile
                  enqueueSnackbar(
                    "Este Menú, tiene perfil/es asignados, no se puede eliminar.",
                    {
                      variant: "error",
                    }
                  );
                } else {
                  //success
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

Tables.propTypes = {
  className: PropTypes.string,
  products: PropTypes.array.isRequired,
};

Tables.defaultProps = {
  products: [],
};

export default Tables;
