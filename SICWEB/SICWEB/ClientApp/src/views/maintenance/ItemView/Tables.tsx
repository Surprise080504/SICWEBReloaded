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
  Download,
} from "react-feather";

import SearchIcon2 from "@material-ui/icons/Search";
import AddIcon2 from "@material-ui/icons/Add";

import type { Theme } from "src/theme";
import type { Product } from "src/types/product";
import NewItem from "./NewItem";
import {
  getSegments,
  getFamilies,
  getUnits,
  getProducts,
  getSubFamilies,
  getItem,
  deleteItem,
  download,
  getPermissionOption,
} from "src/apis/itemApi";
import useSettings from "src/hooks/useSettings";
import ConfirmModal from "src/components/ConfirmModal";
import { useSnackbar } from "notistack";
import { useHistory } from "react-router-dom";
import { getPermission } from "src/apis/userApi";
import _ from "lodash";
import { useSelector } from "react-redux";
import { Print } from "@material-ui/icons";
import jwtDecode from "jwt-decode";

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

  const [segments, setSegments] = useState<any>([]);
  const [products, setProducts] = useState<any>([]);
  const [families, setFamilies] = useState<any>([]);
  const [subFamilies, setSubFamilies] = useState<any>([]);
  const [units, setUnits] = useState<any>([]);

  const [items, setItems] = useState<any>([]);

  const [permissionOption, setPermissionOption] = useState<any>();

  const [filters, setFilters] = useState({
    code: "",
    description: "",
    family: "-1",
    subFamily: "-1",
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
  const [familyPermission, setFamilyPermission] = useState<boolean>(false);
  const [subfamilyPermission, setSubFamilyPermission] =
    useState<boolean>(false);
  useEffect(() => {
    _getInitialData();
    const pathname = history.location?.pathname ?? "";
    const menuItem = getPermission(menuPermission, pathname);
    if (_.isEmpty(menuItem)) {
      history.push("/404");
    }
    setPermission(menuItem);
    _getPermissionOption();
  }, []);

  useEffect(() => {
    let familyAccess = permissionOption?.filter(
      (option) => option.option == "Agregar Familia"
    );
    let subfamilyAccess = permissionOption?.filter(
      (option) => option.option == "Agregar Subfamilia"
    );

    setFamilyPermission(familyAccess?.length > 0 ? false : true);
    setSubFamilyPermission(subfamilyAccess?.length > 0 ? false : true);
  }, [permissionOption]);

  const _getPermissionOption = async () => {
    const decoded: any = jwtDecode(localStorage.getItem("accessToken"));
    let user = decoded.unique_name;
    getPermissionOption(user).then((res) => {
      setPermissionOption(res);
    });
  };

  const _getInitialData = () => {
    _getSegments();
    _getProducts();
    _getFamilies();
    _getUnits();
    handleSearch();
  };

  const _getSegments = () => {
    getSegments().then((res) => {
      setSegments(res);
    });
  };

  const _getProducts = () => {
    getProducts().then((res) => {
      setProducts(res);
    });
  };

  const _getFamilies = () => {
    getFamilies().then((res) => {
      setFamilies(res);
    });
  };

  const _getSubFamilies = (fid) => {
    getSubFamilies(fid)
      .then((res) => {
        setSubFamilies(res);
      })
      .catch((err) => {
        setSubFamilies([]);
      });
  };

  const _getUnits = () => {
    getUnits().then((res) => {
      setUnits(res);
    });
  };

  const handleModalClose = (): void => {
    setIsModalOpen(false);
  };

  const handleSearch = () => {
    getItem(filters)
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

  const handlePageChange = (event: any, newPage: number): void => {
    setPage(newPage);
  };

  return (
    <Card className={clsx(classes.root, className)} {...rest}>
      <Box p={3} alignItems="center">
        <Grid container spacing={3}>
          <Grid item lg={4} sm={6} xs={12}>
            <Grid container spacing={3}>
              <Grid item lg={6} sm={6} xs={12}>
                <TextField
                  fullWidth
                  size="small"
                  label="Código"
                  placeholder=""
                  variant="outlined"
                  value={filters.code}
                  onChange={(e) =>
                    setFilters({ ...filters, code: e.target.value })
                  }
                />
              </Grid>
              <Grid item lg={6} sm={6} xs={12}>
                <TextField
                  fullWidth
                  size="small"
                  label="Descripción"
                  placeholder=""
                  variant="outlined"
                  value={filters.description}
                  onChange={(e) =>
                    setFilters({ ...filters, description: e.target.value })
                  }
                />
              </Grid>
              <Grid item lg={6} sm={6} xs={12}>
                <TextField
                  fullWidth
                  size="small"
                  label="Familia"
                  select
                  SelectProps={{ native: true }}
                  variant="outlined"
                  value={filters.family || -1}
                  onChange={(e) => {
                    setFilters({
                      ...filters,
                      family: e.target.value,
                      subFamily: "-1",
                    });
                    _getSubFamilies(e.target.value);
                  }}
                >
                  <option key="-1" value="-1">
                    {"-- Seleccionar --"}
                  </option>
                  {families.map((family) => (
                    <option key={family.ifm_c_iid} value={family.ifm_c_iid}>
                      {family.ifm_c_des}
                    </option>
                  ))}
                </TextField>
              </Grid>
              <Grid item lg={6} sm={6} xs={12}>
                <TextField
                  fullWidth
                  size="small"
                  label="SubFamilia"
                  name="availability"
                  select
                  SelectProps={{ native: true }}
                  variant="outlined"
                  value={filters.subFamily || -1}
                  onChange={(e) =>
                    setFilters({ ...filters, subFamily: e.target.value })
                  }
                >
                  <option key="-1" value="-1">
                    {"-- Seleccionar --"}
                  </option>

                  {subFamilies.map((subFamily) => (
                    <option
                      key={subFamily.isf_c_iid}
                      value={subFamily.isf_c_iid}
                    >
                      {subFamily.isf_c_vdesc}
                    </option>
                  ))}
                </TextField>
              </Grid>
            </Grid>
          </Grid>
          <Grid item lg={4} sm={3} xs={12}>
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
              <Grid item>
                <Button
                  variant="contained"
                  color="secondary"
                  startIcon={<Download />}
                  onClick={() => download()}
                >
                  {"Descargar"}
                </Button>
              </Grid>
              <Grid item>
                <Button
                  variant="contained"
                  color="secondary"
                  startIcon={<Print />}
                  onClick={() => window.print()}
                >
                  {"Imprimir"}
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
                <TableCell>CÓDIGO</TableCell>
                <TableCell>DESCRIPCIÓN</TableCell>
                <TableCell>PRECIO COMPRA</TableCell>
                <TableCell>PRECIO VENTA</TableCell>
                <TableCell>UNIDAD DE MEDIDA</TableCell>
                <TableCell>FAMILIA</TableCell>
                <TableCell>SUBFAMILIA</TableCell>
                <TableCell align="right">&nbsp;</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {paginatedItems.map((item, index) => {
                return (
                  <TableRow style={{ height: 30 }} hover key={item.itm_c_iid}>
                    <TableCell>{item.itm_c_ccodigo}</TableCell>
                    <TableCell>{item.itm_c_vdescripcion}</TableCell>
                    <TableCell>{item.itm_c_dprecio_compra}</TableCell>
                    <TableCell>{item.itm_c_dprecio_venta}</TableCell>
                    <TableCell>{item.und_c_vdesc}</TableCell>
                    <TableCell>{item.ifm_c_des}</TableCell>
                    <TableCell>{item.isf_c_vdesc}</TableCell>
                    <TableCell align="right">
                      {permission?.perf_menu_c_cmod == "A" && (
                        <Tooltip title="Editar" aria-label="Editar">
                          <IconButton onClick={() => handleEdit(index)}>
                            <SvgIcon fontSize="small">
                              <EditIcon />
                            </SvgIcon>
                          </IconButton>
                        </Tooltip>
                      )}
                      {permission?.perf_menu_c_cvisual == "A" && (
                        <Tooltip title="Eliminar" aria-label="Eliminar">
                          <IconButton
                            onClick={() => handleDelete(item.itm_c_iid)}
                          >
                            <SvgIcon fontSize="small">
                              <DeleteIcon />
                            </SvgIcon>
                          </IconButton>
                        </Tooltip>
                      )}
                      {/*{permission?.perf_menu_c_cvisual == "A" && (*/}
                      {/*    <Tooltip title="Descargar" aria-label="Descargar">*/}
                      {/*        <IconButton onClick={() => window.print()}>*/}
                      {/*            <SvgIcon fontSize="small">*/}
                      {/*                <Download />*/}
                      {/*            </SvgIcon>*/}
                      {/*        </IconButton>*/}
                      {/*    </Tooltip>*/}
                      {/*)}*/}
                      {/*{permission?.perf_menu_c_cvisual == "A" && (*/}
                      {/*    <Tooltip title="Descargar 2" aria-label="Descargar 2">*/}
                      {/*        <IconButton onClick={() => download()}>*/}
                      {/*            <SvgIcon fontSize="small">*/}
                      {/*                <Download />*/}
                      {/*            </SvgIcon>*/}
                      {/*        </IconButton>*/}
                      {/*    </Tooltip>*/}
                      {/*)}*/}
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
          <NewItem
            segments={segments}
            products={[products]}
            families={families}
            subFamilies={subFamilies}
            familyperm={familyPermission}
            subfamilyperm={subfamilyPermission}
            imagefile={
              editID > -1
                ? "estilo_item_image_" + items[editID].itm_c_iid + ".png"
                : null
            }
            units={units}
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
        title={"¿Está seguro de eliminar este Item?"}
        setOpen={() => setIsModalOpen2(false)}
        onConfirm={() => {
          saveSettings({ saving: true });
          window.setTimeout(() => {
            deleteItem(deleteID)
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
                  if (res === 2) {
                    //have parent menu
                    enqueueSnackbar("Este Menú sirve de base para pantallas", {
                      variant: "error",
                    });
                    alert(res);
                  } else if (res === 1) {
                    //have profile
                    enqueueSnackbar("1", {
                      variant: "error",
                    });
                  } else {
                    enqueueSnackbar("Tus datos se han guardado exitosamente.", {
                      variant: "success",
                    });

                    setIsModalOpen2(false);
                    handleSearch();
                  }
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
