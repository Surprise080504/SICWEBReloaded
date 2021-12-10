import { useEffect, useState } from 'react';
import type {
    FC
} from 'react';
import clsx from 'clsx';
import PropTypes from 'prop-types';
import PerfectScrollbar from 'react-perfect-scrollbar';
import {
    Box,
    Typography,
    Button,
    Card,
    Checkbox,
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableRow,
    TextField,
    Tabs,
    Tab,
    makeStyles,
    Dialog,
    Grid,
    IconButton,
    SvgIcon,
    Tooltip,
    TablePagination,
    AppBar,
    Container
} from '@material-ui/core';
import {
    Edit as EditIcon,
    Trash as DeleteIcon,
    Search as SearchIcon
} from 'react-feather';

import SearchIcon2 from '@material-ui/icons/Search';
import AddIcon2 from '@material-ui/icons/Add';

import type { Theme } from 'src/theme';
import type { Product } from 'src/types/product';
import { getCentrosCosto, getAddresses } from 'src/apis/empresaApi';
import useSettings from 'src/hooks/useSettings';
import ConfirmModal from 'src/components/ConfirmModal';
import { useSnackbar } from 'notistack';
import { TabContext, TabList, TabPanel } from '@material-ui/lab';

interface TablesProps {
    className?: string;
    products: Product[];
}

interface Filters {
    availability?: 'available' | 'unavailable';
    category?: string;
    inStock?: boolean;
    isShippable?: boolean;
}

const sortOptions = [
    {
        value: 'updatedAt|desc',
        label: 'Last update (newest first)'
    },
    {
        value: 'updatedAt|asc',
        label: 'Last update (oldest first)'
    },
    {
        value: 'createdAt|desc',
        label: 'Creation date (newest first)'
    },
    {
        value: 'createdAt|asc',
        label: 'Creation date (oldest first)'
    }
];

const applyPaginationAddresses = (items: any[], page: number, limit: number): any[] => {
    return items.slice(page * limit, page * limit + limit);
};

const applyPaginationCentrosCosto = (items: any[], page: number, limit: number): any[] => {
    return items.slice(page * limit, page * limit + limit);
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
    },
}));


const Tables: FC<TablesProps> = ({ className, ...rest }) => {
    const classes = useStyles();
    const { enqueueSnackbar } = useSnackbar();
    const { settings, saveSettings } = useSettings();

    const [values, setValues] = useState({
        direction: settings.direction,
        responsiveFontSizes: settings.responsiveFontSizes,
        theme: settings.theme
    });

    const [centrosCosto, setCentrosCosto] = useState<any>([]);
    const [addresses, setAddresses] = useState<any>([]);
    const [company, setCompany] = useState<any>([]);
    const [value, setValue] = useState("1");

    const [items, setItems] = useState<any>([]);

    const [filters, setFilters] = useState({
        code: '',
        description: '',
        family: '-1',
        subFamily: '-1',
    });

    const [isModalOpen, setIsModalOpen] = useState(false);

    const [deleteID, setDeleteID] = useState('-1');
    const [editID, setEditID] = useState(-1);

    const [isModalOpen2, setIsModalOpen2] = useState(false);

    const [page, setPage] = useState<number>(0);
    const [limit] = useState<number>(15);

    const paginatedItemsAddresses = applyPaginationAddresses(addresses, page, limit);
    const paginatedItemsCentrosCosto = applyPaginationCentrosCosto(centrosCosto, page, limit);



    useEffect(() => {
        _getInitialData();
    }, [])

    const _getInitialData = () => {
        //_getCompany();
        _getAddresses();
        _getCentrosCosto();
    }

    //const _getCompany = () => {
    //    getCompany(1).then(res => {
    //        setCompany(res);
    //    });
    //}

    const _getAddresses = () => {
        getAddresses().then(res => {
            setAddresses(res);
        });
    }

    const _getCentrosCosto = () => {
        getCentrosCosto().then(res => {
            setCentrosCosto(res);
        });
    }

    const handleModalClose = (): void => {
        setIsModalOpen(false);
    };

    const handleDelete = (id) => {
        setDeleteID(id);
        setIsModalOpen2(true);
    }

    const handleEdit = (id) => {
        setEditID(id);
        setIsModalOpen(true);

    }

    const handlePageChange = (event: any, newPage: number): void => {
        setPage(newPage);
    };

    const handleTabChange = (event: React.ChangeEvent<{}>, newValue: string) => {
        setValue(newValue);
    };

    return (
        <Card
            className={clsx(classes.root, className)}
            {...rest}
        >
            <Box p={3} alignItems="center">
                <Grid container spacing={3}>
                    <Grid item lg={6} sm={6} xs={12}>
                        <TextField
                            fullWidth
                            size="small"
                            label="RUC"
                            placeholder="RUC"
                            variant="outlined"
                            value={filters.code}
                            disabled={true}
                            onChange={(e) => setFilters({ ...filters, code: e.target.value })}
                        />
                    </Grid>
                    <Grid item lg={6} sm={6} xs={12}>
                        <TextField
                            fullWidth
                            size="small"
                            label="Razón Social"
                            placeholder="Razón Social"
                            variant="outlined"
                            value={filters.description}
                            onChange={(e) => setFilters({ ...filters, description: e.target.value })}
                        />
                    </Grid>
                </Grid>
            </Box>
            <PerfectScrollbar>
                <Box p={3}>
                    <Grid container>
                        <Grid item container lg={12} sm={12} xs={12}>
                            <TabContext value={value}>
                                <AppBar position="static">
                                    <TabList onChange={handleTabChange} aria-label="simple tabs example">
                                        <Tab label="Centro Costo" value="1" />
                                        <Tab label="Direcciones" value="2" />
                                    </TabList>
                                </AppBar>
                                <Grid container>
                                    <Grid className="GridTab" item xs sm md lg spacing={0}>
                                        <TabPanel value="1" style={{ padding: 0 }}>
                                            <Box>
                                                <Table
                                                    stickyHeader >
                                                    <TableHead style={{ background: 'red' }}>
                                                        <TableRow>
                                                            <TableCell>CÓDIGO</TableCell>
                                                            <TableCell>DESCRIPCIÓN</TableCell>
                                                            <TableCell>SERIE BOLETA</TableCell>
                                                            <TableCell>SERIE FACTURA</TableCell>
                                                            <TableCell align="right">&nbsp;</TableCell>
                                                        </TableRow>
                                                    </TableHead>
                                                    <TableBody>
                                                        {paginatedItemsCentrosCosto.map((item, index) => {
                                                            return (
                                                                <TableRow
                                                                    style={{ height: 30 }}
                                                                    hover
                                                                    key={item.emp_cst_c_iid}
                                                                >
                                                                    <TableCell>
                                                                        {item.emp_cst_c_iid}
                                                                    </TableCell>
                                                                    <TableCell>
                                                                        {item.emp_cst_c_vdesc}
                                                                    </TableCell>
                                                                    <TableCell>
                                                                        {item.emp_cst_c_vserieboleta}
                                                                    </TableCell>
                                                                    <TableCell>
                                                                        {item.emp_cst_c_vseriefactura}
                                                                    </TableCell>
                                                                    <TableCell align="right">
                                                                        <Tooltip title="Editar" aria-label="Editar">
                                                                            <IconButton onClick={() => handleEdit(index)}>
                                                                                <SvgIcon fontSize="small">
                                                                                    <EditIcon />
                                                                                </SvgIcon>
                                                                            </IconButton>
                                                                        </Tooltip>
                                                                        <Tooltip title="Eliminar" aria-label="Eliminar">
                                                                            <IconButton onClick={() => handleDelete(item.itm_c_iid)}>
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
                                                    count={items.length}
                                                    onPageChange={handlePageChange}
                                                    onRowsPerPageChange={() => { }}
                                                    page={page}
                                                    rowsPerPage={limit}
                                                    rowsPerPageOptions={[15]}
                                                />
                                            </Box>
                                        </TabPanel>
                                        <TabPanel value="2" style={{ padding: 0 }}>
                                            Item Two
                                        </TabPanel>
                                    </Grid>
                                </Grid>
                            </TabContext>

                        </Grid>
                    </Grid>

                </Box>

            </PerfectScrollbar>
            <Dialog
                maxWidth="md"
                fullWidth
                onClose={handleModalClose}
                open={isModalOpen}
            >
                {/* Dialog renders its body even if not open */}
                {isModalOpen}
            </Dialog>

        </Card>
    );
};


Tables.propTypes = {
    className: PropTypes.string,
    products: PropTypes.array.isRequired
};

Tables.defaultProps = {
    products: []
};

export default Tables;