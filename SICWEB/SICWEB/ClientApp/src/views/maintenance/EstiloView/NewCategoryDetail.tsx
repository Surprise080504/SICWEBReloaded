import { FC, useState } from "react";
import * as Yup from "yup";
import { Formik } from "formik";
import {
  Box,
  Typography,
  TextField,
  Button,
  Divider,
  FormControlLabel,
  Switch,
  Grid,
} from "@material-ui/core";
import { useSnackbar } from "notistack";
import SaveIcon3 from "@material-ui/icons/Save";
import useSettings from "src/hooks/useSettings";
import { saveCategoryDetail } from "src/apis/styleApi";

interface NewCategoryDetailProps {
  event?: Event;
  onCancel?: () => void;
}

const NewCategoryDetail: FC<NewCategoryDetailProps> = ({ onCancel }) => {
  const { enqueueSnackbar } = useSnackbar();
  const { saveSettings } = useSettings();

  return (
    <>
      <Box p={3}>
        <Typography
          align="center"
          gutterBottom
          variant="h4"
          color="textPrimary"
        >
          Nuevo Categoría
        </Typography>
      </Box>
      <Divider />
      <>
        <Box p={3}>
          <Formik
            initialValues={{
              category: "",
              description: "",
            }}
            validationSchema={Yup.object().shape({
              category: Yup.string()
                .max(20, "Debe tener 20 caracteres como máximo")
                .required("Se requiere un nombre o código de categoría."),
              description: Yup.string()
                .max(50, "Debe tener 50 caracteres como máximo")
                .required("Se requiere una descripción de categoría."),
            })}
            onSubmit={(values, { resetForm }) => {
              saveSettings({ saving: true });
              window.setTimeout(() => {
                saveCategoryDetail(values)
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
                <Grid container spacing={3}>
                  <Grid item lg={12} sm={12} xs={12}>
                    <TextField
                      size="small"
                      fullWidth
                      label={
                        <label>
                          Categoría<span style={{ color: "red" }}>*</span>
                        </label>
                      }
                      name="category"
                      onBlur={handleBlur}
                      onChange={handleChange}
                      error={Boolean(touched.category && errors.category)}
                      helperText={touched.category && errors.category}
                      value={values.category}
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
                          Description<span style={{ color: "red" }}>*</span>
                        </label>
                      }
                      name="description"
                      onBlur={handleBlur}
                      onChange={handleChange}
                      error={Boolean(touched.description && errors.description)}
                      helperText={touched.description && errors.description}
                      value={values.description}
                      variant="outlined"
                      InputLabelProps={{
                        shrink: true,
                      }}
                    />
                  </Grid>
                  <Grid
                    item
                    lg={12}
                    sm={12}
                    xs={12}
                    style={{ display: "flex" }}
                  >
                    <Button
                      type="submit"
                      size="small"
                      color="secondary"
                      startIcon={<SaveIcon3 />}
                      variant="contained"
                    >
                      GUARDAR
                    </Button>
                  </Grid>
                </Grid>
              </form>
            )}
          </Formik>
        </Box>
        <Divider />
      </>

      <Box p={2} display="flex" alignItems="center">
        <Box flexGrow={1} />
        <Button onClick={onCancel}>{"Cancelar"}</Button>
      </Box>
    </>
  );
};

export default NewCategoryDetail;
