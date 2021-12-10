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
  TextField,
  Card,
  Button
} from "@material-ui/core";
import SearchIcon from "@material-ui/icons/Search";
import KeyboardReturnIcon from '@material-ui/icons/KeyboardReturn';
import PerfectScrollbar from "react-perfect-scrollbar";
import { getOrdens } from "src/apis/entradaApi";
import { getEstado, getMoneda } from "src/apis/comprasApi";
import { format } from 'date-fns';

interface OrdenTableProps {
  closeModal: Function,
  selectOrden: Function
}

const applyPagination = (
  data: any,
  page: number,
  limit: number
): any[] => {
  return data.slice(page * limit, page * limit + limit);
};

const OrdenTable: FC<OrdenTableProps> = ({ closeModal, selectOrden }) => {

  const [ordens, setOrdens] = useState<any>([]);
  const [monedaOptions, setMonedaOptions] = useState<any>([]);
  const [estados, setEstados] = useState<any>([]);
  const [filters, setFilters] = useState({
    ruc: "",
    moneda: -1,
    estado: -1,
  });

  const [page, setPage] = useState<number>(0);
  const [limit] = useState<number>(15);

  const paginatedItems = applyPagination(ordens, page, limit);

  const handlePageChange = (event: any, newPage: number): void => {
    setPage(newPage);
  };

  const handleSearch = () => {
    getOrdens(filters)
      .then((res) => {
        setOrdens(res);
      })
      .catch((err) => {
        setOrdens([]);
      });
  };

  const _getEstados = () => {
    getEstado()
      .then((res) => {
        setEstados(res);
      })
      .catch((err) => {
        setEstados([]);
      });
  }

  const _getMoneda = () => {
    getMoneda()
      .then((res) => {
        setMonedaOptions(res);
      })
      .catch((err) => {
        setMonedaOptions([]);
      });
  }

  const _getInitialData = async () => {
    await _getEstados();
    await _getMoneda();
    await handleSearch();
  }

  useEffect(() => {
    _getInitialData();
  }, []);

  const handleSelect = (id, serie, codigo, prov) => {
    selectOrden(id, serie, codigo, prov);
  }

  return (
    <Card>
      <Box p={3} alignItems="center">
        <Grid container spacing={1}>
          <Grid item sm={8} xs={12}>
            <Grid container spacing={1}>
              <Grid item sm={4} xs={12}>
                <TextField
                  fullWidth
                  size="small"
                  label="RUC Prov."
                  placeholder="RUC Prov."
                  variant="outlined"
                  value={filters.ruc}
                  onChange={(e) =>
                    setFilters({ ...filters, ruc: e.target.value })
                  }
                />
              </Grid>
              <Grid item sm={4} xs={12}>
                <TextField
                  fullWidth
                  size="small"
                  label="Moneda"
                  placeholder=""
                  variant="outlined"
                  value={filters.moneda}
                  SelectProps={{ native: true }}
                  select
                  onChange={(e) => {
                    setFilters({
                      ...filters,
                      moneda: Number(e.target.value),
                    });
                  }}
                >
                  <option key="-1" value="-1">
                    {"-- Seleccionar --"}
                  </option>
                  {monedaOptions.map((option) => (
                    <option
                      key={option.par_det_c_iid}
                      value={option.par_det_c_iid}
                    >
                      {option.par_det_c_vdesc}
                    </option>
                  ))}
                </TextField>
              </Grid>
              <Grid item sm={4} xs={12}>
                <TextField
                  fullWidth
                  size="small"
                  label="Estado"
                  placeholder=""
                  variant="outlined"
                  value={filters.estado}
                  SelectProps={{ native: true }}
                  select
                  onChange={(e) => {
                    setFilters({
                      ...filters,
                      estado: Number(e.target.value),
                    });
                  }}
                >
                  <option key="-1" value="-1">
                    {"-- Seleccionar --"}
                  </option>
                  {estados.map((estadooption) => (
                    <option
                      key={estadooption.odc_estado_iid}
                      value={estadooption.odc_estado_iid}
                    >
                      {estadooption.odc_estado_vdescripcion}
                    </option>
                  ))}
                </TextField>
              </Grid>
            </Grid>
          </Grid>
          <Grid item></Grid>
          <Grid item sm={3} xs={12}>
            <Grid container spacing={3}>
              <Grid item sm={6} xs={12}>
                <Button
                  onClick={handleSearch}
                  variant="contained"
                  color="primary"
                  startIcon={<SearchIcon />}
                >
                  {"Buscar"}
                </Button>
              </Grid>
              <Grid item sm={6} xs={12}>
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
          </Grid>
        </Grid>
      </Box>
      <PerfectScrollbar>
        <Box style={{ width: '100%' }}>
          <Table stickyHeader>
            <TableHead style={{ background: "red" }}>
              <TableRow>
                <TableCell>Serie</TableCell>
                <TableCell>Código</TableCell>
                <TableCell>RUC</TableCell>
                <TableCell>Proveedor</TableCell>
                <TableCell>Estado</TableCell>
                <TableCell>Moneda</TableCell>
                <TableCell>Registro</TableCell>
                <TableCell>Entrega Inicio</TableCell>
                <TableCell>Entrega Fin</TableCell>
                <TableCell>Total</TableCell>
                <TableCell align="right">&nbsp;</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {paginatedItems.map((item, index) => {
                return (
                  <TableRow style={{ height: 30 }} hover key={index}>
                    <TableCell>{item.serie}</TableCell>
                    <TableCell>{item.codigo}</TableCell>
                    <TableCell>{item.ruc}</TableCell>
                    <TableCell>{item.prov}</TableCell>
                    <TableCell>{item.estado}</TableCell>
                    <TableCell>{item.moneda}</TableCell>
                    <TableCell>{format(new Date(item.fechaRegistro), 'dd/MM/yyyy')}</TableCell>
                    <TableCell>{format(new Date(item.fechaEntregaIni), 'dd/MM/yyyy')}</TableCell>
                    <TableCell>{format(new Date(item.fechaEntregaFin), 'dd/MM/yyyy')}</TableCell>
                    <TableCell>{item.monototal}</TableCell>
                    <TableCell align="right">
                      <Button
                        onClick={() => handleSelect(item.id, item.serie, item.codigo, item.prov)}
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
            count={ordens.length}
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

export default OrdenTable;
