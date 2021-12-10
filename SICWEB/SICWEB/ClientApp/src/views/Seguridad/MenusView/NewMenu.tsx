import { useEffect, useState } from "react";
import type { FC } from "react";
import PropTypes from "prop-types";

import _ from "lodash";
import * as Yup from "yup";
import { Formik } from "formik";
import {
  Box,
  Typography,
  TextField,
  Button,
  IconButton,
  Divider,
  FormHelperText,
  makeStyles,
  Grid,
  Dialog,
} from "@material-ui/core";
import AddIcon2 from "@material-ui/icons/Add";
import NewOPC from "./NewOPC";
import type { Theme } from "src/theme";
import type { Event } from "src/types/calendar";
import { getParentMenus, saveMenu, getOPCs } from "src/apis/menuApi";
import { useSnackbar } from "notistack";
import useSettings from "src/hooks/useSettings";
import Multiselect from "multiselect-react-dropdown";
import { RssFeedTwoTone } from "@material-ui/icons";

interface NewMenuProps {
  editID: number;
  parent_id: number;
  _initialValue?: any;
  menu?: any[];
  parent_menu?: any[];
  nivel?: any[];
  pagina?: any[];
  opciones?: number;
  estado?: number;
  profileid?: number;
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
    marginLeft: theme.spacing(2),
  },
}));

const NewMenu: FC<NewMenuProps> = ({
  editID,
  parent_id,
  _initialValue,
  menu,
  parent_menu,
  nivel,
  pagina,
  opciones,
  estado,
  event,
  profileid,
  _getInitialData,
  onAddComplete,
  onCancel,
  onDeleteComplete,
  onEditComplete,
}) => {
  const classes = useStyles();
  const { enqueueSnackbar } = useSnackbar();
  const { saveSettings } = useSettings();
  const [isModalOpen3, setIsModalOpen3] = useState(false);
  const [parentMenus, setParentMenus] = useState<any>([]);

  const [vdesc, setVdesc] = useState<any>([]);
  const [bestado, setBestado] = useState<any>([]);

  const [opcs, setOPCs] = useState<any>([]);
  const [selectedopcs, setSelectedopcs] = useState<any>([]);

  const [modalState, setModalState] = useState(0);

  const [menuInfo, setMenuInfo] = useState<any>({});

  const [ispaginaenable, setIsPaginaEnable] = useState<boolean>(true);

  const _getParentMenus = () => {
    getParentMenus().then((res) => {
      setParentMenus(res);
    });
  };

  const handleModalClose3 = (): void => {
    setIsModalOpen3(false);
    if (editID > -1) {
      _getOPCs(_initialValue[editID].menu_c_iid);
    }
  };

  const handleModalOpen3 = (): void => {
    setIsModalOpen3(true);
  };

  const handleModalClose = (): void => {
    setIsModalOpen3(false);
  };

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

  const nivelOptions = [
    {
      value: 0,
      label: "-- Seleccione --",
    },
    {
      value: 1,
      label: "Menú",
    },
    {
      value: 2,
      label: "Pantalla",
    },
  ];

  const onSelect = (selectedList, selectedItem) => {
    setSelectedopcs(selectedList);
  };

  const onRemove = (selectedList, removedItem) => {
    setSelectedopcs(selectedList);
  };

  const getInitialValues = () => {
    if (editID > -1) {
      return _.merge(
        {},
        {
          id: -1,
          parent_id: -1,
          menu: "",
          parent_menu: "",
          nivel: "",
          pagina: "",
          opciones: "",
          estado: 1,
          submit: null,
        },
        {
          id: _initialValue[editID].menu_c_iid,
          parent_id: _initialValue[editID].parent_menu_c_iid,
          menu: _initialValue[editID].menu_c_vnomb,
          parent_menu: _initialValue[editID].parent_menu_c_vnomb,
          nivel: _initialValue[editID].menu_c_ynivel,
          pagina:
            _initialValue[editID].menu_c_ynivel == 1
              ? ""
              : _initialValue[editID].menu_c_vpag_asp,
          opciones: _initialValue[editID].opciones,
          estado: _initialValue[editID].estado,
          submit: null,
        }
      );
    } else {
      return {
        id: -1,
        parent_id: -1,
        menu: "",
        parent_menu: "",
        nivel: "",
        pagina: "",
        opciones: "",
        estado: 1,
        submit: null,
      };
    }
  };

  const _getOPCs = (id) => {
    getOPCs(id, profileid).then((res: []) => {
      let optionData = [];
      let selectedoptiondata = [];
      res.map((data) => {
        optionData.push({ name: data["opc_c_vdesc"], id: data["opc_c_iid"] });
        if (data["checkedValue"] == 1) {
          selectedoptiondata.push({
            name: data["opc_c_vdesc"],
            id: data["opc_c_iid"],
          });
        }
      });
      setOPCs(optionData);
      setSelectedopcs(selectedoptiondata);
    });
  };

  const handleItemChange = (e: any): void => {
    if (e.target.name === "nivel") {
      if (Number(e.target.value) === 2) {
        _getOPCs(-1);
        setIsPaginaEnable(false);
      } else {
        menuInfo["pagina"] = "";
        setIsPaginaEnable(true);
      }
    }

    var data = { [e.target.name]: e.target.value };
    setMenuInfo({ ...menuInfo, ...data });
  };

  useEffect(() => {
    _getParentMenus();
    if (editID > -1) {
      _getOPCs(_initialValue[editID].menu_c_iid);
    } else {
      _getOPCs(-1);
    }

    if (editID > -1) {
      if (_initialValue[editID].menu_c_ynivel == 1) {
        setIsPaginaEnable(true);
      } else {
        setIsPaginaEnable(false);
      }
    }

    const iniVal = getInitialValues();
    if (iniVal) {
      setMenuInfo({
        ...iniVal,
      });
    }
  }, []);

  useEffect(() => {
    if (editID > -1) {
      _getOPCs(_initialValue[editID].menu_c_iid);
    } else {
      _getOPCs(-1);
    }
  }, [isModalOpen3]);

  return (
    <>
      <Formik
        initialValues={getInitialValues()}
        validationSchema={Yup.object().shape({
          menu: Yup.string()
            .max(200, "Debe tener 200 caracteres como máximo")
            .required("El menú es obligatorio."),
          pagina: !ispaginaenable
            ? Yup.string()
                .max(200, "Debe tener 200 caracteres como máximo.")
                .required("La página es obligatoria.")
            : null,
          nivel:
            menuInfo.nivel < 1
              ? Yup.number().min(1).required("El nivel es obligatorio.")
              : null,
        })}
        onSubmit={async (
          values,
          { resetForm, setErrors, setStatus, setSubmitting }
        ) => {
          saveSettings({ saving: true });
          menuInfo.menu = values.menu;
          menuInfo.pagina = values.pagina;
          values = { ...values, ...menuInfo };

          window.setTimeout(() => {
            values.estado = Number(values?.estado) === 1 ? true : false;
            values.nivel = values?.nivel === "" ? 0 : values?.nivel;
            if (values?.nivel == 2) {
              values.opciones = selectedopcs;
            }
            saveMenu(values)
              .then((res) => {
                saveSettings({ saving: false });
                _getInitialData();
                enqueueSnackbar("Tus datos se han guardado exitosamente.", {
                  variant: "success",
                });
                resetForm();
                setStatus({ success: true });
                setSubmitting(false);
                onCancel();
              })
              .catch((err) => {
                _getInitialData();
                enqueueSnackbar("No se pudo guardar.", {
                  variant: "error",
                });
                saveSettings({ saving: false });
              });
          }, 1000);
        }}
      >
        {({
          errors,
          handleBlur,
          handleSubmit,
          handleChange,
          isSubmitting,
          setFieldTouched,
          setFieldValue,
          touched,
          values,
        }) => (
          <form onSubmit={handleSubmit}>
            <Box p={3}>
              <Typography
                align="center"
                gutterBottom
                variant="h4"
                color="textPrimary"
              >
                {editID > -1 ? "Editar Menú" : "Nuevo Menú"}
              </Typography>
            </Box>
            <Divider />
            <Box p={3}>
              <Grid container spacing={3}>
                <Grid item lg={12} sm={12} xs={12}>
                  <TextField
                    size="small"
                    error={Boolean(touched.menu && errors.menu)}
                    fullWidth
                    helperText={touched.menu && errors.menu}
                    label={
                      <label>
                        Menú <span style={{ color: "red" }}>*</span>
                      </label>
                    }
                    name="menu"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    value={values.menu}
                    variant="outlined"
                    InputLabelProps={{
                      shrink: true,
                    }}
                  />
                </Grid>
                <Grid item lg={12} sm={12} xs={12}>
                  <TextField
                    size="small"
                    fullWidth
                    label={<label>Menú Padre</label>}
                    name="parent_id"
                    SelectProps={{ native: true }}
                    select
                    onBlur={handleBlur}
                    onChange={(e) => {
                      _getParentMenus();
                      handleItemChange(e);
                    }}
                    value={menuInfo.parent_id}
                    variant="outlined"
                    InputLabelProps={{
                      shrink: true,
                    }}
                  >
                    {<option key="-1" value="-1"></option>}
                    {parentMenus.map((parentMenu) => (
                      <option
                        selected
                        key={parentMenu.menu_c_iid}
                        value={parentMenu.menu_c_iid}
                      >
                        {parentMenu.menu_c_vnomb}
                      </option>
                    ))}
                  </TextField>
                </Grid>
                <Grid item lg={12} sm={12} xs={12} style={{ display: "flex" }}>
                  <TextField
                    size="small"
                    error={Boolean(touched.nivel && errors.nivel)}
                    helperText={touched.nivel && errors.nivel}
                    label={
                      <label>
                        Nivel <span style={{ color: "red" }}>*</span>
                      </label>
                    }
                    name="nivel"
                    fullWidth
                    SelectProps={{ native: true }}
                    select
                    variant="outlined"
                    onBlur={handleBlur}
                    onChange={handleItemChange}
                    value={menuInfo.nivel}
                    InputLabelProps={{
                      shrink: true,
                    }}
                  >
                    {nivelOptions.map((nivel) => (
                      <option key={nivel.value} value={nivel.value}>
                        {nivel.label}
                      </option>
                    ))}
                  </TextField>
                </Grid>
              </Grid>
              <Grid container spacing={3}>
                <Grid item lg={12} sm={12} xs={12}>
                  <TextField
                    size="small"
                    error={Boolean(touched.pagina && errors.pagina)}
                    fullWidth
                    helperText={touched.pagina && errors.pagina}
                    label={
                      <label>
                        Página <span style={{ color: "red" }}>*</span>
                      </label>
                    }
                    name="pagina"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    value={values.pagina}
                    variant="outlined"
                    style={{ display: ispaginaenable ? "none" : "block" }}
                    InputLabelProps={{
                      shrink: true,
                    }}
                  />
                </Grid>
              </Grid>
              <Grid container spacing={3}>
                <Grid
                  item
                  lg={12}
                  sm={12}
                  xs={12}
                  style={{ display: ispaginaenable ? "none" : "flex" }}
                >
                  <Multiselect
                    options={opcs} // Options to display in the dropdown
                    selectedValues={selectedopcs} // Preselected value to persist in dropdown
                    onSelect={onSelect} // Function will trigger on select event
                    onRemove={onRemove} // Function will trigger on remove event
                    displayValue="name" // Property name to display in the dropdown options
                  />
                  <IconButton
                    size="small"
                    color="secondary"
                    aria-label="add to shopping cart"
                    onClick={() => {
                      setModalState(0);
                      handleModalOpen3();
                    }}
                  >
                    <AddIcon2 />
                  </IconButton>
                </Grid>
                <Grid item lg={12} sm={12} xs={12}>
                  <TextField
                    size="small"
                    fullWidth
                    SelectProps={{ native: true }}
                    select
                    label={<label>Estado</label>}
                    name="estado"
                    onBlur={handleBlur}
                    onChange={handleItemChange}
                    value={menuInfo.estado}
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
            </Box>
            <Divider />
            {errors.submit && (
              <Box mt={3}>
                <FormHelperText error>{errors.submit}</FormHelperText>
              </Box>
            )}
            <Box p={2} display="flex" alignItems="center">
              <Box flexGrow={1} />
              <Button onClick={onCancel}>{"Cancelar"}</Button>
              <Button
                variant="contained"
                type="submit"
                disabled={isSubmitting}
                color="secondary"
                className={classes.confirmButton}
              >
                {"Confirmar"}
              </Button>
            </Box>
          </form>
        )}
      </Formik>
      <Dialog
        maxWidth="md"
        fullWidth
        onClose={handleModalClose3}
        open={isModalOpen3}
      >
        {/* Dialog renders its body even if not open */}
        {isModalOpen3 && (
          <NewOPC
            vdesc={vdesc}
            bestado={bestado}
            onAddComplete={handleModalClose3}
            onCancel={handleModalClose3}
            onDeleteComplete={handleModalClose3}
            onEditComplete={handleModalClose3}
          />
        )}
      </Dialog>
    </>
  );
};

NewMenu.propTypes = {
  // @ts-ignore
  event: PropTypes.object,
  onAddComplete: PropTypes.func,
  onCancel: PropTypes.func,
  onDeleteComplete: PropTypes.func,
  onEditComplete: PropTypes.func,
  // @ts-ignore
  range: PropTypes.object,
};

export default NewMenu;
