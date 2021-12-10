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
import type { Theme } from "src/theme";
import type { Event } from "src/types/calendar";
import { saveProcess } from "src/apis/processApi";
import { useSnackbar } from "notistack";
import useSettings from "src/hooks/useSettings";

interface NewProcessProps {
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

const NewProcess: FC<NewProcessProps> = ({
  event,
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

  const [modalState, setModalState] = useState(0);

  const handleModalClose3 = (): void => {
    setIsModalOpen3(false);
  };

  const handleModalOpen3 = (): void => {
    setIsModalOpen3(true);
  };
  const getInitialValues = () => {
    return {
      cid: "",
      vdesc: "",
      submit: null,
    };
  };

  return (
    <>
      <Formik
        initialValues={getInitialValues()}
        validationSchema={Yup.object().shape({
          cid: Yup.string()
            .max(50, "Debe tener 50 caracteres como máximo")
            .required("El proceso es requerido"),
          vdesc: Yup.string()
            .max(50, "Debe tener 50 caracteres como máximo")
            .required("Se requiere la descripcion"),
        })}
        onSubmit={async (
          values,
          { resetForm, setErrors, setStatus, setSubmitting }
        ) => {
          saveSettings({ saving: true });
          window.setTimeout(() => {
            saveProcess(values)
              .then((res) => {
                saveSettings({ saving: false });
                enqueueSnackbar("Tus datos se han guardado exitosamente.", {
                  variant: "success",
                });
                onCancel();
              })
              .catch((err) => {
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
          handleChange,
          handleSubmit,
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
                Nuevo Proceso
              </Typography>
            </Box>
            <Divider />
            <Box p={3}>
              <Grid container spacing={3}>
                <Grid item lg={12} sm={12} xs={12}>
                  <TextField
                    size="small"
                    fullWidth
                    label={
                      <label>
                        Proceso <span style={{ color: "red" }}>*</span>
                      </label>
                    }
                    name="cid"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    value={values.cid}
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
                    label={
                      <label>
                        Descripción<span style={{ color: "red" }}>*</span>
                      </label>
                    }
                    name="vdesc"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    value={values.vdesc}
                    variant="outlined"
                    InputLabelProps={{
                      shrink: true,
                    }}
                  />
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
    </>
  );
};

NewProcess.propTypes = {
  // @ts-ignore
  event: PropTypes.object,
  onAddComplete: PropTypes.func,
  onCancel: PropTypes.func,
  onDeleteComplete: PropTypes.func,
  onEditComplete: PropTypes.func,
  // @ts-ignore
  range: PropTypes.object,
};

export default NewProcess;
