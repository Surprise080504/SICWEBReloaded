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
  Divider,
  FormHelperText,
  makeStyles,
  Grid,
} from "@material-ui/core";
import type { Theme } from "src/theme";
import type { Event } from "src/types/calendar";
import useSettings from "src/hooks/useSettings";

interface NewChildItemProps {
  event?: Event;
  onCancel?: () => void;
  handleAddChild: Function;
}

const useStyles = makeStyles((theme: Theme) => ({
  root: {},
  confirmButton: {
    marginLeft: theme.spacing(2),
  },
}));

const NewChildItem: FC<NewChildItemProps> = ({ onCancel, handleAddChild }) => {
  const classes = useStyles();
  const { saveSettings } = useSettings();
  const getInitialValues = () => {
    return {
      cost: null,
      effot: null,
      vdesc: "",
      submit: null,
    };
  };

  return (
    <>
      <Formik
        initialValues={getInitialValues()}
        validationSchema={Yup.object().shape({
          cost: Yup.number().min(0).required("Este campo es obligatorio"),
          effot: Yup.number().min(0).required("Este campo es obligatorio"),
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
            handleAddChild(values);
            resetForm();
            onCancel();
            saveSettings({ saving: false });
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
                Nuevo Artículo
              </Typography>
            </Box>
            <Divider />
            <Box p={3}>
              <Grid container spacing={3}>
                <Grid item lg={12} sm={12} xs={12}>
                  <TextField
                    error={Boolean(touched.vdesc && errors.vdesc)}
                    helperText={touched.vdesc && errors.vdesc}
                    size="small"
                    fullWidth
                    label={
                      <label>
                        Descripción <span style={{ color: "red" }}>*</span>
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
                <Grid item lg={12} sm={12} xs={12}>
                  <TextField
                    error={Boolean(touched.cost && errors.cost)}
                    helperText={touched.cost && errors.cost}
                    size="small"
                    fullWidth
                    label={
                      <label>
                        Costo<span style={{ color: "red" }}>*</span>
                      </label>
                    }
                    name="cost"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    type="number"
                    value={values.cost}
                    variant="outlined"
                    InputLabelProps={{
                      shrink: true,
                    }}
                  />
                </Grid>
                <Grid item lg={12} sm={12} xs={12}>
                  <TextField
                    error={Boolean(touched.effot && errors.effot)}
                    helperText={touched.effot && errors.effot}
                    size="small"
                    fullWidth
                    label={
                      <label>
                        Esfuerzo en<span style={{ color: "red" }}>*</span>
                      </label>
                    }
                    name="effot"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    type="number"
                    value={values.effot}
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

NewChildItem.propTypes = {
  // @ts-ignore
  event: PropTypes.object,
  onAddComplete: PropTypes.func,
  onCancel: PropTypes.func,
  onDeleteComplete: PropTypes.func,
  onEditComplete: PropTypes.func,
  // @ts-ignore
  range: PropTypes.object,
};

export default NewChildItem;
