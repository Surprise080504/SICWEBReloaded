import { useEffect, useState } from "react";
import type { FC } from "react";
import clsx from "clsx";
import PropTypes from "prop-types";
import PerfectScrollbar from "react-perfect-scrollbar";
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
import { Edit as EditIcon, Trash as DeleteIcon } from "react-feather";

import SearchIcon2 from "@material-ui/icons/Search";
import AddIcon2 from "@material-ui/icons/Add";

import type { Theme } from "src/theme";
import type { Product } from "src/types/product";
import NewUser from "./NewUser";
import { getUser, deleteUser, getPermission } from "src/apis/userApi";
import useSettings from "src/hooks/useSettings";
import ConfirmModal from "src/components/ConfirmModal";
import { useSnackbar } from "notistack";
import { resourceUsage } from "process";
import { useHistory } from "react-router-dom";
import _ from "lodash";
import { useSelector } from "react-redux";

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

interface responseItem {
  user: string;
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

  const [items, setItems] = useState<any>([]);

  const [filters, setFilters] = useState({
    user: "",
    name: "",
    surname: "",
    state: -1,
    networkuser: "",
  });

  const [isModalOpen, setIsModalOpen] = useState(false);

  const [deleteID, setDeleteID] = useState("-1");
  const [editID, setEditID] = useState(-1);

  const [isModalOpen2, setIsModalOpen2] = useState(false);

  const [page, setPage] = useState<number>(0);
  const [limit] = useState<number>(15);

  const paginatedItems = applyPagination(items, page, limit);

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

  const _getInitialData = () => {
    handleSearch();
  };

  const handleModalClose = (): void => {
    setIsModalOpen(false);
  };

  const handleSearch = () => {
    setPage(0);
    getUser(filters)
      .then((res: []) => {
        let newArr = [];
        (Array.isArray(res) ? res : []).map((element: responseItem) => {
          const existCheck = newArr.filter(
            (item) => item?.user === element.user
          )?.[0]
            ? true
            : false;
          if (!existCheck) newArr.push(element);
        });
        setItems(newArr);
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

  const handlePageChange = (event: any, newPage: number): void => {
    setPage(newPage);
  };

  return (
    <Card className={clsx(classes.root, className)} {...rest}>
      <Box p={3} alignItems="center">
        <Grid container spacing={3}>
          <Grid item lg={8} sm={6} xs={12}>
            <Grid container spacing={3}>
              <Grid item lg={2} sm={6} xs={12}>
                <TextField
                  fullWidth
                  size="small"
                  label="Usuario"
                  placeholder=""
                  variant="outlined"
                  value={filters.user}
                  onChange={(e) =>
                    setFilters({ ...filters, user: e.target.value })
                  }
                />
              </Grid>
              <Grid item lg={2} sm={6} xs={12}>
                <TextField
                  fullWidth
                  size="small"
                  label="Usuario de red"
                  placeholder=""
                  variant="outlined"
                  value={filters.networkuser}
                  onChange={(e) =>
                    setFilters({ ...filters, networkuser: e.target.value })
                  }
                />
              </Grid>
              <Grid item lg={3} sm={6} xs={12}>
                <TextField
                  fullWidth
                  size="small"
                  label="Nombres"
                  placeholder=""
                  variant="outlined"
                  value={filters.name}
                  onChange={(e) =>
                    setFilters({ ...filters, name: e.target.value })
                  }
                />
              </Grid>
              <Grid item lg={3} sm={6} xs={12}>
                <TextField
                  fullWidth
                  size="small"
                  label="Apellidos"
                  placeholder=""
                  variant="outlined"
                  value={filters.surname}
                  onChange={(e) =>
                    setFilters({ ...filters, surname: e.target.value })
                  }
                />
              </Grid>
              <Grid item lg={2} sm={6} xs={12}>
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
          <Grid item lg={2} sm={3} xs={12}>
            <></>
          </Grid>
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
                <TableCell>Usuario</TableCell>
                <TableCell>Usuario de red</TableCell>
                <TableCell>Nombres</TableCell>
                <TableCell>Apellido Paterno</TableCell>
                <TableCell>Apellido Materno</TableCell>
                <TableCell>Perfiles</TableCell>
                <TableCell>Roles</TableCell>
                <TableCell>Rol Principal</TableCell>
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
                  <TableRow style={{ height: 30 }} hover key={index}>
                    <TableCell>{item.user}</TableCell>
                    <TableCell>{item.networkuser}</TableCell>
                    <TableCell>{item.name}</TableCell>
                    <TableCell>{item.lastname}</TableCell>
                    <TableCell>{item.mlastname}</TableCell>
                    <TableCell>{item.profile}</TableCell>
                    <TableCell>{item.role}</TableCell>
                    <TableCell>{item.roleprinciple}</TableCell>
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
                          <IconButton onClick={() => handleDelete(item.user)}>
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
          <NewUser
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
      <ConfirmModal
        open={isModalOpen2}
        title={"¿Está seguro de eliminar este usuario?"}
        setOpen={() => setIsModalOpen2(false)}
        onConfirm={() => {
          saveSettings({ saving: true });
          window.setTimeout(() => {
            deleteUser(deleteID)
              .then((res) => {
                saveSettings({ saving: false });
                _getInitialData();
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
