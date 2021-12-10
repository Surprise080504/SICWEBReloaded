import { useEffect, useState } from 'react';
import type { FC } from 'react';
import PropTypes from 'prop-types';

import _ from 'lodash';
import PerfectScrollbar from 'react-perfect-scrollbar';
import {
    Box,
    Card,
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableRow,
    makeStyles,
    IconButton,
    SvgIcon,
    Tooltip,
    TablePagination,
    Typography,
    Button,
    Divider
} from '@material-ui/core';
import AddIcon2 from '@material-ui/icons/Add';
import type { Theme } from 'src/theme';
import type { Event } from 'src/types/calendar';
import type { Opc } from 'src/types/opc';
import { getOpcsForView} from 'src/apis/menuApi';
import { useSnackbar } from 'notistack';
import useSettings from 'src/hooks/useSettings';


interface OPCViewProps {
    editID: number,
    event?: Event;
    _getInitialData?: () => void;
    onAddComplete?: () => void;
    onCancel?: () => void;
    onDeleteComplete?: () => void;
    onEditComplete?: () => void;
}

const useStyles = makeStyles((theme: Theme) => ({
    root: {},
    confirmButton: {
        marginLeft: theme.spacing(2)
    }
}));


const OPCView: FC<OPCViewProps> = ({
    editID,
    event,
    _getInitialData,
    onAddComplete,
    onCancel,
    onDeleteComplete,
    onEditComplete
}) => {
    const classes = useStyles();
    const { enqueueSnackbar } = useSnackbar();
    const { saveSettings } = useSettings();
    const [isModalOpen3, setIsModalOpen3] = useState(false);
    const [parentMenus, setParentMenus] = useState<any>([]);
    const [modalState, setModalState] = useState(0);    
    const [opcs, setOPCs] = useState<any>([]);

    const [page, setPage] = useState<number>(0);
    const [limit] = useState<number>(15);

    const applyPagination = (products: any[], page: number, limit: number): any[] => {
        return products.slice(page * limit, page * limit + limit);
    };

    const paginatedItems = applyPagination(opcs, page, limit);

    const _getOPCs = (id) => {
        getOpcsForView(id).then(res => {
            setOPCs(res);
        })
    }

    const handleModalClose3 = (): void => {
        setIsModalOpen3(false);
    };

    const handleModalOpen3 = (): void => {
        setIsModalOpen3(true);
    };

    const handlePageChange = (event: any, newPage: number): void => {
        setPage(newPage);
    };

    useEffect(() => {
        _getOPCs(editID);
    }, [])

    return (
        <>
            <Card>
                <PerfectScrollbar>
                    <Box p={3}>
                        <Typography
                            align="center"
                            gutterBottom
                            variant="h4"
                            color="textPrimary"
                        >
                            Opciones
                        </Typography>
                    </Box>
                    <Divider />
                    <Box minWidth={400}>
                    <Table
                        stickyHeader >
                        <TableHead style={{background: 'red'}}>
                        <TableRow>
                            <TableCell>
                            vdesc
                            </TableCell>
                            <TableCell>
                            bestado
                            </TableCell>
                        </TableRow>
                        </TableHead>
                        <TableBody>
                        {paginatedItems.map((item, index) => {
                            return (
                            <TableRow
                            style={{height: 30 }}
                                hover
                                key={item.opc_c_iid}
                            >
                                <TableCell>
                                {item.opc_c_vdesc}
                                </TableCell>
                                <TableCell>
                                {item.opc_c_bestado == 1 ? "Activo" : "Inactivo"}
                                </TableCell>
                            </TableRow>
                            );
                        })}
                        </TableBody>
                    </Table>
                    <TablePagination
                        component="div"
                        count={opcs.length}
                                onPageChange={handlePageChange}
                                onRowsPerPageChange={() => { }}
                        page={page}
                        rowsPerPage={limit}
                        rowsPerPageOptions={[15]}
                    />
                    </Box>
                    <Divider />
                    <Box
                        p={2}
                        display="flex"
                        alignItems="center"
                    >
                        <Box flexGrow={1} />
                        <Button 
                            color="secondary"
                            className={classes.confirmButton}
                            onClick={onCancel}
                            variant="contained">
                            {'Cerrar'}
                        </Button>
                        </Box>
                </PerfectScrollbar>
            </Card>
        </>
    );
};

OPCView.propTypes = {
    // @ts-ignore
    event: PropTypes.object,
    onAddComplete: PropTypes.func,
    onCancel: PropTypes.func,
    onDeleteComplete: PropTypes.func,
    onEditComplete: PropTypes.func,
    // @ts-ignore
    range: PropTypes.object
};

export default OPCView;
