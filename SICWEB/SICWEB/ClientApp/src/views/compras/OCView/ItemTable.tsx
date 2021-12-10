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
import {
  getSegments,
  getFamilies,
  getUnits,
  getProducts,
  getSubFamilies,
  getItemChecked,
} from "src/apis/itemApi";
import useSettings from "src/hooks/useSettings";
import ConfirmModal from "src/components/ConfirmModal";
import { useSnackbar } from "notistack";
import { useHistory } from "react-router-dom";
import _ from "lodash";
import { useSelector } from "react-redux";
import { Print } from "@material-ui/icons";

interface ItemTableProps {
  _getCheckedValue?: (res: any) => void;
  onCancel?: () => void;
  className?: string;
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

const ItemTable: FC<ItemTableProps> = ({
  _getCheckedValue,
  onCancel,
  className,
  ...rest
}) => {
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

  const [checkedValue, setCheckedValue] = useState<any>();

  const handleMultiSelectorChange = (e) => {
    if (e.target.name.indexOf(".check") > -1) {
      var data = { [e.target.name]: e.target.checked };

      let newItem = items.map((item, index) => {
        if (
          index == e.target.name.replace("items[", "").replace("].check", "")
        ) {
          item.checkstate = e.target.checked;
        }
        return item;
      });
      setCheckedValue(newItem);
    }
  };

  useEffect(() => {
    _getInitialData();
  }, []);

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
    getItemChecked(filters)
      .then((res) => {
        setItems(res);
      })
      .catch((err) => {
        setItems([]);
      });
  };

  const handleRegister = () => {
    _getCheckedValue(checkedValue);
    onCancel();
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
          <Grid item lg={8} sm={8} xs={12}>
            <Grid container spacing={3}>
              <Grid item lg={3} sm={3} xs={12}>
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
              <Grid item lg={3} sm={3} xs={12}>
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
              <Grid item lg={3} sm={3} xs={12}>
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
              <Grid item lg={3} sm={3} xs={12}>
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
          <Grid item lg={4} sm={4} xs={12}>
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
                  onClick={handleRegister}
                  variant="contained"
                  color="primary"
                >
                  {"Regresar"}
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
                    <TableCell>
                      <Checkbox
                        checked={item.check}
                        onChange={handleMultiSelectorChange}
                        name={`items[${index}].check`}
                      />
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
    </Card>
  );
};

ItemTable.propTypes = {
  _getCheckedValue: PropTypes.func,
  className: PropTypes.string,
};

ItemTable.defaultProps = {};

export default ItemTable;
