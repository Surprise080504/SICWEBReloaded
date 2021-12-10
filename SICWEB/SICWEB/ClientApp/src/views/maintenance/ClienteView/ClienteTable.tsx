import { useEffect, useState } from 'react';
import type {
  FC} from 'react';
import clsx from 'clsx';
import PropTypes from 'prop-types';
import PerfectScrollbar from 'react-perfect-scrollbar';
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
  FormControlLabel,
  IconButton,
  SvgIcon,
  Tooltip,
  TablePagination
} from '@material-ui/core';
import {
  Edit as EditIcon,
  Trash as DeleteIcon,
  Search as SearchIcon
} from 'react-feather';
import SearchIcon2 from '@material-ui/icons/Search';
import AddIcon2 from '@material-ui/icons/Add';

import type { Theme } from 'src/theme';
import {getClients, saveCliente, deleteCliente} from 'src/apis/clienteApi';
import useSettings from 'src/hooks/useSettings';
import { useSnackbar } from 'notistack';
import NewItem from './NewItem';
interface TablesProps {
  className?: string;
}
const applyPagination = (clientes: any[], page: number, limit: number): any[] => {
  return clientes.slice(page * limit, page * limit + limit);
};
const useStyles = makeStyles((theme: Theme) => ({
  root: {},
  bulkOperations: {
    position: 'relative'
  },
  bulkActions: {
    paddingLeft: 4,
    paddingRight: 4,
    marginTop: 6,
    position: 'absolute',
    width: '100%',
    zIndex: 2,
    backgroundColor: theme.palette.background.default
  },
  bulkAction: {
    marginLeft: theme.spacing(2)
  },
  queryField: {
    width: 200
  },
  queryFieldMargin: {
    marginLeft: theme.spacing(2),
  },
  categoryField: {
    width: 200,
    flexBasis: 200
  },
  availabilityField: {
    width: 200,
    marginLeft: theme.spacing(2),
    flexBasis: 200
  },
  buttonBox: {
    '& > *': {
      margin: theme.spacing(1),
    },
  },
  stockField: {
    marginLeft: theme.spacing(2)
  },
  shippableField: {
    marginLeft: theme.spacing(2)
  },
  imageCell: {
    fontSize: 0,
    width: 68,
    flexBasis: 68,
    flexGrow: 0,
    flexShrink: 0
  },
  image: {
    height: 68,
    width: 68
  }
}));
const ClienteTable: FC<TablesProps> = ({ className, ...rest }) => {
  const classes = useStyles();
  const [clients, setClients] = useState<any>([]);
  const [filters, setFilters] = useState({
    company: '',
    ruc: '',
    client: true,
    provider: false,
  });
  const [deleteID, setDeleteID] = useState('-1');
  const [editID, setEditID] = useState(-1);
  const [isModalOpen, setIsModalOpen] = useState(false);

  const [page, setPage] = useState<number>(0);
  const [limit] = useState<number>(15);
  const paginatedClients = applyPagination(clients, page, limit);
  useEffect(() => {
    _getInitialData();
  }, [])
  const _getInitialData = () => {
    handleSearch();
  }
  const handleModalClose = (): void => {
    setIsModalOpen(false);
  };
  const handleSearch =() => {
    getClients(filters).then(res => {
      setClients(res);
    }).catch(err => {
      setClients([]);
    })
  }
  const handleDelete =(id) => {
    //setDeleteID(id);
    //setIsModalOpen2(true);
  }
  const handleEdit =(id) => {
    setEditID(id);
    setIsModalOpen(true);
  }
  const handlePageChange = (event: any, newPage: number): void => {
    setPage(newPage);
  };
  const handleClienteChange=(event:any):void=>{
    setFilters({...filters, client: event.target.checked});
    handleSearch();
  }
  const handleProveedorChange=(event:any):void=>{
    setFilters({...filters, provider: event.target.checked});
  }
  return (
    <Card
      className={clsx(classes.root, className)}
      {...rest}
    >
      <Box p={3} alignItems="center">
        <Grid container spacing={3}>
          <Grid item lg={8} sm={6} xs={12}>
            <Grid container spacing={3}>
              <Grid item lg={4} sm={6} xs={12}>
                <TextField
                  fullWidth
                  size="small"
                  label="Razón Social"
                  placeholder="Razón Social"
                  variant="outlined"
                  value={filters.company}
                  onChange={(e) => setFilters({...filters, company: e.target.value})}
                />
              </Grid>
              <Grid item lg={4} sm={6} xs={12}>
                <TextField
                  fullWidth
                  size="small"
                  label="RUC"
                  placeholder="RUC"
                  variant="outlined"
                  value={filters.ruc}
                  onChange={(e) => setFilters({...filters, ruc: e.target.value})}
                />
              </Grid>
              <Grid item lg={4} sm={6} xs={12}>
                <FormControlLabel
                  className={classes.shippableField}
                  control={(
                    <Checkbox
                      checked={filters.client}
                      onChange={handleClienteChange}
                      name="Cliente"
                    />
                  )}
                  label="Cliente"
                />
                <FormControlLabel
                  className={classes.shippableField}
                  control={(
                    <Checkbox
                      checked={filters.provider}
                      onChange={handleProveedorChange}
                      name="Proveedor"
                    />
                  )}
                  label="Proveedor"
                />
              </Grid>
            </Grid>
          </Grid>
          <Grid item lg={4} sm={3} xs={12}>
            <Grid container spacing={3}>
              <Grid item>
                <Button onClick={handleSearch} variant="contained" color="primary" startIcon={<SearchIcon2 />}>{'Buscar'}</Button>
              </Grid>
              <Grid item>
                <Button variant="contained" color="secondary" startIcon={<AddIcon2 />} onClick={() => handleEdit('-1')}>{'Nuevo'}</Button>
              </Grid>
            </Grid>
          </Grid>      
        </Grid>
      </Box>  
      <PerfectScrollbar>
        <Box minWidth={1200}>
          <Table
            stickyHeader >
            <TableHead style={{background: 'red'}}>
              <TableRow>
                <TableCell>
                RUC
                </TableCell>
                <TableCell>
                RAZÓN SOCIAL
                </TableCell>
                <TableCell>
                RUBRO
                </TableCell>
                <TableCell>
                PROVEEDOR / CLIENTE
                </TableCell>
                <TableCell align="right">
                  &nbsp;
                </TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {paginatedClients.map((item, index) => {
                return (
                  <TableRow
                   style={{height: 30 }}
                    hover
                    key={item.ruc}
                  >
                    <TableCell>
                     {item.ruc}
                    </TableCell>
                    <TableCell>
                     {item.company}
                    </TableCell>
                    <TableCell>
                     {item.ditem}
                    </TableCell>
                    <TableCell>
                      <FormControlLabel
                        className={classes.shippableField}
                        control={(
                          <Checkbox
                            checked={item.provider}
                          />
                        )}
                        label=""
                      />
                      <FormControlLabel
                        className={classes.shippableField}
                        control={(
                          <Checkbox
                            checked={item.client}
                          />
                        )}
                        label=""
                      />
                    </TableCell>
                    <TableCell align="right">
                      <Tooltip title="Editar" aria-label="Editar">
                        <IconButton onClick={() =>handleEdit(index)}>
                          <SvgIcon fontSize="small">
                            <EditIcon />
                          </SvgIcon>
                        </IconButton>
                      </Tooltip>
                      <Tooltip title="Eliminar" aria-label="Eliminar">
                        <IconButton onClick={() =>handleDelete(item.itm_c_iid)}>
                          <SvgIcon fontSize="small">
                            <DeleteIcon />
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
            count={clients.length}
            onPageChange={handlePageChange}
            onRowsPerPageChange={() => { }}
            page={page}
            rowsPerPage={limit}
            rowsPerPageOptions={[15]}
          />
        </Box>
      </PerfectScrollbar>
      <Dialog
        maxWidth="lg"
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
            units={[]}
            _getInitialData={_getInitialData}
            editID = {editID}
            _initialValue = {[]}
            onAddComplete={handleModalClose}
            onCancel={handleModalClose}
            onDeleteComplete={handleModalClose}
            onEditComplete={handleModalClose}
          />
        )}
      </Dialog>
    </Card>
  );
};
ClienteTable.propTypes = {
  className: PropTypes.string,
};
ClienteTable.defaultProps = {
};
export default ClienteTable;
