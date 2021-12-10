import { useEffect, useState } from "react";
import type { FC } from "react";
import {
  Box,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
  TablePagination,
  Grid,
  Card,
  Button,
  TextField
} from "@material-ui/core";
import KeyboardReturnIcon from '@material-ui/icons/KeyboardReturn';
import SearchIcon from "@material-ui/icons/Search";
import PerfectScrollbar from "react-perfect-scrollbar";
import { getAlmacens } from "src/apis/entradaApi";

interface OrdenTableProps {
  closeModal: Function,
  selectAlmacen: Function
}

const applyPagination = (
  data: any,
  page: number,
  limit: number
): any[] => {
  return data.slice(page * limit, page * limit + limit);
};

const AlmacenTable: FC<OrdenTableProps> = ({ closeModal, selectAlmacen }) => {

  const [almacens, setAlmacens] = useState<any>([]);
  const [filters, setFilters] = useState({
    descripcion: ""
  });

  const [page, setPage] = useState<number>(0);
  const [limit] = useState<number>(15);

  const paginatedItems = applyPagination(almacens, page, limit);

  const handlePageChange = (event: any, newPage: number): void => {
    setPage(newPage);
  };

  const handleSearch = () => {
    getAlmacens(filters)
      .then((res: any) => {
        setAlmacens(res);
      })
      .catch((err) => {
        setAlmacens([]);
      });
  }

  useEffect(() => {
    handleSearch();
  }, []);

  return (
    <Card>
      <Box p={3} alignItems="center">
        <Grid container spacing={1} style={{ display: 'flex', justifyContent: 'space-between' }}>
          <Grid item xs={12} sm={5}>
            <TextField
              fullWidth
              size="small"
              label="Descripcion"
              placeholder="Descripcion"
              variant="outlined"
              value={filters.descripcion}
              onChange={(e) =>
                setFilters({ ...filters, descripcion: e.target.value })
              }
            />
          </Grid>
          <Grid item>
            <Button
              onClick={handleSearch}
              variant="contained"
              color="primary"
              startIcon={<SearchIcon />}
            >
              {"Buscar"}
            </Button>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <Button
              variant="contained"
              color="secondary"
              startIcon={<KeyboardReturnIcon />}
              onClick={() => closeModal()}
            >
              {"Regresar"}
            </Button>
          </Grid>
        </Grid>
      </Box>
      <PerfectScrollbar>
        <Box style={{ width: '100%' }}>
          <Table stickyHeader>
            <TableHead style={{ background: "red" }}>
              <TableRow>
                <TableCell align="center">Almacén</TableCell>
                <TableCell align="right">&nbsp;</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {paginatedItems.map((item, index) => {
                return (
                  <TableRow style={{ height: 30 }} hover key={index}>
                    <TableCell align="center">{item.descripcion}</TableCell>
                    <TableCell align="right">
                      <Button
                        onClick={() => selectAlmacen(item.id, item.descripcion)}
                        variant="contained"
                        color="secondary"
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
            count={almacens.length}
            onPageChange={handlePageChange}
            onRowsPerPageChange={() => { }}
            page={page}
            rowsPerPage={limit}
            rowsPerPageOptions={[15]}
          />
        </Box>
      </PerfectScrollbar>
    </Card>
  );
};

export default AlmacenTable;
