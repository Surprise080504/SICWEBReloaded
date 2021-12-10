import { useEffect, useState } from "react";
import type { FC } from "react";
import {
  Box,
  Typography,
  TextField,
  Button,
  Divider,
  makeStyles,
  Grid,
  Dialog
} from "@material-ui/core";
import Backdrop from '@material-ui/core/Backdrop';
import CircularProgress from '@material-ui/core/CircularProgress';
import AddIcon2 from "@material-ui/icons/Add";
import type { Theme } from "src/theme";
import ItemTable from "./ItemTable";
import OrdernTable from "./OrdernTable";
import AlmacenTable from "./AlmacenTable";
import { getItems, getItemsById, getAlmacenById, getOrdenById, getDetailEntradaById } from "src/apis/entradaApi";

interface NewEntradaProps {
  currentID: number,
  closeModal: Function,
  saveEntrada: Function,
  mode: boolean
}

const useStyles = makeStyles((theme: Theme) => ({
  root: { flexGrow: 1 },
  confirmButton: {
    marginLeft: theme.spacing(2),
  },
  paper: {
    padding: theme.spacing(2),
    textAlign: 'center',
    color: theme.palette.text.secondary,
  },
  backdrop: {
    zIndex: theme.zIndex.drawer + 1,
    color: '#fff',
  }
}));

const NewEntrada: FC<NewEntradaProps> = ({ currentID, closeModal, saveEntrada, mode }) => {
  const classes = useStyles();

  const [loading, setLoading] = useState(false);
  const [ordenModalOpen, setOrdenModalOpen] = useState<boolean>(false);
  const [almacenModalOpen, setAlmacenModalOpen] = useState<boolean>(false);
  const [ordenData, setOrdenData] = useState({
    serie: '',
    numero: '',
    proveedor: '',
    id: -1
  });
  const [almacenData, setAlmacenData] = useState({
    descripcion: '',
    id: -1
  })
  const [facturaData, setFacturaData] = useState({
    serie: '',
    numero: '',
    fecha: ''
  });
  const [guiaData, setGuiaData] = useState({
    serie: '',
    numero: '',
    fecha: ''
  });
  const [observacion, setObservacion] = useState<string>('');
  const [items, setItems] = useState<any>([]);

  const _getItems = (id) => {
    getItems(id)
      .then((res: any) => {
        setItems(res);
      })
      .catch((err) => {
        setItems([]);
      });
  }

  const handleSelectOrden = async (id, serie, numero, proveedor) => {
    await setLoading(true);
    await setOrdenData({
      id,
      serie,
      numero,
      proveedor
    });
    await setOrdenModalOpen(false);
    await _getItems(id);
    await setLoading(false);
  }

  const handleSelectAlmacen = (id, descripcion) => {
    setAlmacenData({
      id,
      descripcion
    });
    setAlmacenModalOpen(false);
  }

  const handleOrdenModalClose = () => {
    setOrdenModalOpen(false);
  }

  const handleAlmacenModalClose = () => {
    setAlmacenModalOpen(false);
  }

  const handleSubmit = (e) => {
    e.preventDefault();
    const saveData = {
      mve_c_iid: currentID,
      odc_c_iid: ordenData.id,
      mve_c_zguiafecha: guiaData.fecha ? guiaData.fecha : null,
      mve_c_zfacturafecha: facturaData.fecha,
      mve_c_vguiacodigo: String(guiaData.serie).concat('-', String(guiaData.numero)),
      mve_c_vfacturacodigo: String(facturaData.serie).concat('-', String(facturaData.numero)),
      mve_c_iidalmacen: almacenData.id,
      mve_c_iestado: 2,
      mve_c_vdesestado: "POR REGULARIZAR",
      mve_c_vobservacion: observacion,
      items: items
    }
    saveEntrada(saveData);
  }

  const _getItemsById = () => {
    getItemsById(currentID)
      .then((res: any) => {
        setItems(res);
      })
      .catch((err) => {
        setItems([]);
      });
  }

  const _getAlmacenById = () => {
    getAlmacenById(currentID)
      .then((res: any) => {
        setAlmacenData({
          id: res.alm_c_iid,
          descripcion: res.alm_c_vdesc
        });
      })
      .catch((err) => {
        setAlmacenData({
          id: -1,
          descripcion: ""
        });
      });
  }

  const _getOrdenById = () => {
    getOrdenById(currentID)
      .then((res: any) => {
        setOrdenData(res);
      })
      .catch((err) => {
        setOrdenData({
          serie: '',
          numero: '',
          proveedor: '',
          id: -1
        });
      });
  }

  const _getDetailEntradaById = () => {
    getDetailEntradaById(currentID)
      .then((res: any) => {
        setFacturaData({
          serie: String(res.mve_c_vfacturacodigo).split("-")[0],
          numero: String(res.mve_c_vfacturacodigo).split("-")[1],
          fecha: String(res.mve_c_zfacturafecha).split("T")[0]
        });
        setGuiaData({
          serie: String(res.mve_c_vguiacodigo).split("-")[0],
          numero: String(res.mve_c_vguiacodigo).split("-")[1],
          fecha: res.mve_c_zguiafecha ? String(res.mve_c_zguiafecha).split("T")[0] : ''
        });
        setObservacion(String(res.mve_c_vobservacion));
      })
      .catch((err) => {
        setOrdenData({
          serie: '',
          numero: '',
          proveedor: '',
          id: -1
        });
      });
  }

  const _getInitialData = async () => {
    await setLoading(true);
    await _getDetailEntradaById();
    await _getOrdenById();
    await _getItemsById();
    await _getAlmacenById();
    await setLoading(false);
  }

  useEffect(() => {
    if (currentID > -1) {
      _getInitialData();
    }
  }, [currentID]);

  const [guiaRequired, setGuiaRequired] = useState<boolean>(false);

  useEffect(() => {
    if (!guiaData.fecha && !guiaData.numero && !guiaData.serie) setGuiaRequired(false);
    else setGuiaRequired(true);
  }, [guiaData]);

  return (
    <>
      <form onSubmit={handleSubmit} autoComplete="off">
        <Box p={1}>
          <Typography
            align="center"
            gutterBottom
            variant="h4"
            color="textPrimary"
          >
            {currentID > -1 ? "Editar Entrada" : "Nuevo Entrada"}
          </Typography>
        </Box>
        <Divider />
        <Box p={3}>
          <div className={classes.root}>
            <Grid container spacing={1}>
              {/*SECTION: Datos de la Orden de Compra */}
              <Grid item xs={12}>
                <Typography variant="body1" gutterBottom>
                  Datos de la Orden de Compra
                </Typography>
              </Grid>
              <Grid item xs={12} sm={2} container alignItems="center">
                <Typography variant="body2" gutterBottom>
                  Serie - Número
                </Typography>
                <Typography variant="h6" gutterBottom color="error">
                  *
                </Typography>
              </Grid>
              <Grid item xs={12} sm={6} container alignItems="center" spacing={1}>
                <Grid item xs={12} sm={3}>
                  <TextField id="outlined-basic" disabled={mode} InputLabelProps={{ required: false }} required value={ordenData.serie} size="small" fullWidth label="Serie" variant="outlined" />
                </Grid>
                <Grid item>
                  -
                </Grid>
                <Grid item xs={12} sm={6}>
                  <TextField id="outlined-basic" disabled={mode} InputLabelProps={{ required: false }} required value={ordenData.numero} size="small" fullWidth label="Número" variant="outlined" />
                </Grid>
                <Grid item xs={12} sm={2}>
                  <Button
                    onClick={() => setOrdenModalOpen(true)}
                    variant="contained"
                    color="primary"
                    startIcon={<AddIcon2 />}
                    disabled={mode}
                  >
                    {"Buscar"}
                  </Button>
                </Grid>
              </Grid>
              <Grid item xs={12} sm={4}>
                <></>
              </Grid>
              <Grid item xs={12} sm={2} container alignItems="center">
                <Typography variant="body2" gutterBottom>
                  Proveedor
                </Typography>
                <Typography variant="h6" gutterBottom color="error">
                  *
                </Typography>
              </Grid>
              <Grid item xs={12} sm={6} container alignItems="center">
                <Grid item xs={12}>
                  <TextField id="outlined-basic" disabled={mode} InputLabelProps={{ required: false }} required value={ordenData.proveedor} size="small" fullWidth label="Proveedor" variant="outlined" />
                </Grid>
              </Grid>
              <Grid item xs={12} sm={4}>
                <></>
              </Grid>
              {/*SECTION: Datos de la Factura */}
              <Grid item xs={12}>
                <Typography variant="body1" gutterBottom>
                  Datos de la Factura
                </Typography>
              </Grid>
              <Grid item xs={12} sm={2} container alignItems="center">
                <Typography variant="body2" gutterBottom>
                  Serie - Número
                </Typography>
                <Typography variant="h6" gutterBottom color="error">
                  *
                </Typography>
              </Grid>
              <Grid item xs={12} sm={6} container alignItems="center" spacing={1}>
                <Grid item xs={12} sm={3}>
                  <TextField id="outlined-basic" InputLabelProps={{ required: false }} required value={facturaData.serie}
                    onChange={(e) => {
                      if (e.target.value.length < 4)
                        setFacturaData({ ...facturaData, serie: e.target.value })
                    }} size="small" fullWidth label="Serie" variant="outlined" disabled={mode} />
                </Grid>
                <Grid item>
                  -
                </Grid>
                <Grid item xs={12} sm={8}>
                  <TextField id="outlined-basic" InputLabelProps={{ required: false }} required value={facturaData.numero}
                    onChange={(e) => {
                      if (e.target.value.length < 7)
                        setFacturaData({ ...facturaData, numero: e.target.value })
                    }} size="small" fullWidth label="Número" variant="outlined" disabled={mode} />
                </Grid>
              </Grid>
              <Grid item xs={12} sm={4} container alignItems="center" spacing={1}>
                <Grid item xs={12} sm={4} container alignItems="center">
                  <Typography variant="body2" gutterBottom>
                    Fecha de Factura
                  </Typography>
                  <Typography variant="h6" gutterBottom color="error">
                    *
                  </Typography>
                </Grid>
                <Grid item xs={12} sm={8}>
                  <TextField
                    id="date"
                    label="Fecha de Factura"
                    type="date"
                    size="small"
                    variant="outlined"
                    value={facturaData.fecha}
                    InputLabelProps={{
                      shrink: true,
                      required: false
                    }}
                    fullWidth
                    onChange={(e) =>
                      setFacturaData({ ...facturaData, fecha: e.target.value })
                    }
                    required
                    disabled={mode}
                  />
                </Grid>
              </Grid>
              {/*SECTION: Datos de la Guía */}
              <Grid item xs={12}>
                <Typography variant="body1" gutterBottom>
                  Datos de la Guía
                </Typography>
              </Grid>
              <Grid item xs={12} sm={2} container alignItems="center">
                <Typography variant="body2" gutterBottom>
                  Serie - Número
                </Typography>
                {guiaRequired && (
                  <Typography variant="h6" gutterBottom color="error">
                    *
                  </Typography>
                )}
              </Grid>
              <Grid item xs={12} sm={6} container alignItems="center" spacing={1}>
                <Grid item xs={12} sm={3}>
                  <TextField id="outlined-basic" InputLabelProps={{ required: false }} required={guiaRequired} value={guiaData.serie}
                    onChange={(e) => {
                      if (e.target.value.length < 4)
                        setGuiaData({ ...guiaData, serie: e.target.value })
                    }} size="small" fullWidth label="Serie" variant="outlined" disabled={mode} />
                </Grid>
                <Grid item>
                  -
                </Grid>
                <Grid item xs={12} sm={8}>
                  <TextField id="outlined-basic" InputLabelProps={{ required: false }} required={guiaRequired} value={guiaData.numero}
                    onChange={(e) => {
                      if (e.target.value.length < 7)
                        setGuiaData({ ...guiaData, numero: e.target.value })
                    }} size="small" fullWidth label="Número" variant="outlined" disabled={mode} />
                </Grid>
              </Grid>
              <Grid item xs={12} sm={4} container alignItems="center" spacing={1}>
                <Grid item xs={12} sm={4} container alignItems="center">
                  <Typography variant="body2" gutterBottom>
                    Fecha de Guia
                  </Typography>
                  {guiaRequired && (
                    <Typography variant="h6" gutterBottom color="error">
                      *
                    </Typography>
                  )}
                </Grid>
                <Grid item xs={12} sm={8}>
                  <TextField
                    id="date"
                    label="Fecha de Guia"
                    type="date"
                    size="small"
                    variant="outlined"
                    value={guiaData.fecha}
                    InputLabelProps={{
                      shrink: true,
                      required: false
                    }}
                    fullWidth
                    onChange={(e) =>
                      setGuiaData({ ...guiaData, fecha: e.target.value })
                    }
                    required={guiaRequired}
                    disabled={mode}
                  />
                </Grid>
              </Grid>
              {/*SECTION: Datos del Almacén */}
              <Grid item xs={12}>
                <Typography variant="body1" gutterBottom>
                  Datos del Almacén
                </Typography>
              </Grid>
              <Grid item xs={12} sm={2} container alignItems="center">
                <Typography variant="body2" gutterBottom>
                  Almacén
                </Typography>
                <Typography variant="h6" gutterBottom color="error">
                  *
                </Typography>
              </Grid>
              <Grid item xs={12} sm={6} container alignItems="center" spacing={1}>
                <Grid item xs={12} sm={9}>
                  <TextField id="outlined-basic" disabled={mode} InputLabelProps={{ required: false }} required size="small" fullWidth label="Almacén" variant="outlined" value={almacenData.descripcion} />
                </Grid>
                <Grid item xs={12} sm={3}>
                  <Button
                    onClick={() => setAlmacenModalOpen(true)}
                    variant="contained"
                    color="primary"
                    startIcon={<AddIcon2 />}
                    disabled={mode}
                  >
                    {"Buscar"}
                  </Button>
                </Grid>
              </Grid>
              <Grid item xs={12} sm={4}>
                <></>
              </Grid>
              {/*SECTION: Items */}
              <Grid item xs={12} sm={2} container alignItems="center">
                <Typography variant="body1" gutterBottom>
                  Items
                </Typography>
                <Typography variant="h6" gutterBottom color="error">
                  *
                </Typography>
              </Grid>
              <Grid item xs={12} sm={10}>
                <ItemTable mode={mode} data={items} setData={setItems} />
              </Grid>
              {/*SECTION: Observaciones */}
              <Grid item xs={12} sm={2} container alignItems="center">
                <Typography variant="body1" gutterBottom>
                  Observaciones
                </Typography>
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  id="outlined-multiline-static"
                  label="Observaciones"
                  multiline
                  fullWidth
                  size="small"
                  rows={4}
                  variant="outlined"
                  value={observacion}
                  onChange={(e) => setObservacion(e.target.value)}
                  disabled={mode}
                />
              </Grid>
            </Grid>
          </div>
        </Box>
        <Divider />
        <Box p={2} display="flex" alignItems="center">
          <Box flexGrow={1} />
          <Button
            onClick={() => closeModal()}
          >
            {"Cancelar"}
          </Button>
          <Button
            variant="contained"
            type="submit"
            color="secondary"
            className={classes.confirmButton}
            disabled={mode}
          >
            {"Confirmar"}
          </Button>
        </Box>
      </form>
      <Dialog
        maxWidth="md"
        fullWidth
        onClose={handleOrdenModalClose}
        open={ordenModalOpen}
      >
        {/* Dialog renders its body even if not open */}
        {ordenModalOpen && (
          <OrdernTable closeModal={handleOrdenModalClose} selectOrden={handleSelectOrden} />
        )}
      </Dialog>
      <Dialog
        maxWidth="md"
        fullWidth
        onClose={handleAlmacenModalClose}
        open={almacenModalOpen}
      >
        {/* Dialog renders its body even if not open */}
        {almacenModalOpen && (
          <AlmacenTable closeModal={handleAlmacenModalClose} selectAlmacen={handleSelectAlmacen} />
        )}
      </Dialog>
      <Backdrop className={classes.backdrop} open={loading}>
        <CircularProgress color="inherit" />
      </Backdrop>
    </>
  );
};

export default NewEntrada;
