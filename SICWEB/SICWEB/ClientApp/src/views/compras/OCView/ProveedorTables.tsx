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
import { getProveedorChecked } from "src/apis/itemApi";
import useSettings from "src/hooks/useSettings";
import ConfirmModal from "src/components/ConfirmModal";
import { useSnackbar } from "notistack";
import { useHistory } from "react-router-dom";
import _ from "lodash";
import { useSelector } from "react-redux";
import { Print } from "@material-ui/icons";

interface ProveedorTableProps {
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

const applyPagination = (items: any[], page: number, limit: number): any[] => {
  return items.slice(page * limit, page * limit + limit);
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

const ProveedorTable: FC<ProveedorTableProps> = ({
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
  const [items, setItems] = useState<any>([]);

  const [filters, setFilters] = useState({
    ruc: "",
    social: "",
  });

  const [page, setPage] = useState<number>(0);
  const [limit] = useState<number>(15);

  const paginatedItems = applyPagination(items, page, limit);

  useEffect(() => {
    _getInitialData();
  }, []);

  const _getInitialData = () => {
    handleSearch();
  };

  const handleSearch = () => {
    getProveedorChecked(filters)
      .then((res) => {
        setItems(res);
      })
      .catch((err) => {
        setItems([]);
      });
  };

  const handleRegister = (item) => {
    _getCheckedValue(item);
    onCancel();
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
              <Grid item lg={6} sm={6} xs={12}>
                <TextField
                  fullWidth
                  size="small"
                  label="Código"
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
                  label="Descripción"
                  placeholder=""
                  variant="outlined"
                  value={filters.social}
                  onChange={(e) =>
                    setFilters({ ...filters, social: e.target.value })
                  }
                />
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
            </Grid>
          </Grid>
        </Grid>
      </Box>
      <PerfectScrollbar>
        <Box minWidth={600}>
          <Table stickyHeader>
            <TableHead style={{ background: "red" }}>
              <TableRow>
                <TableCell>RUC</TableCell>
                <TableCell>Razón Social</TableCell>
                <TableCell align="right">&nbsp;</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {paginatedItems.map((item, index) => {
                return (
                  <TableRow style={{ height: 30 }} hover key={item.ruc}>
                    <TableCell>{item.ruc}</TableCell>
                    <TableCell>{item.social}</TableCell>
                    <TableCell>
                      <Button
                        onClick={() => {
                          handleRegister(item);
                        }}
                        variant="contained"
                        color="primary"
                      >
                        {"Seleccionar"}
                      </Button>
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

ProveedorTable.propTypes = {
  _getCheckedValue: PropTypes.func,
  className: PropTypes.string,
};

ProveedorTable.defaultProps = {};

export default ProveedorTable;
