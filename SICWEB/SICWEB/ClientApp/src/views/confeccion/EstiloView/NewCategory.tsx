import { FC, useState } from 'react';
import * as Yup from 'yup';
import { Formik } from 'formik';
import {
  Box,
  Typography,
  TextField,
  Button,
  Divider,
  FormControlLabel,
  Switch,
  Grid
} from '@material-ui/core';
import { useSnackbar } from 'notistack';
import SaveIcon3 from '@material-ui/icons/Save';
import useSettings from 'src/hooks/useSettings';
import { getFamilies1, getSubFamilies, saveFamily, saveSubFamily, saveUnit } from 'src/apis/itemApi';
import { saveCategory, saveColor } from 'src/apis/styleApi';

interface NewCategoryProps {
    modalState?: any,
    brands?: any[],
    originColors?: any[],
    originCategories?: any[],
    event?: Event;
    setDefaultData?: () => void;
    onCancel?: () => void;
}

const NewCategory: FC<NewCategoryProps> = ({
  modalState,
  brands,
  originColors,
  originCategories,
  setDefaultData,
  onCancel,
}) => {
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
				{
					modalState === 0 && 'Marca'
				}
				{
					modalState === 1 && 'Color de Marca'
				}
				{
					modalState === 2 && 'Categoría de Marca'
				}
            </Typography>
          </Box>
          <Divider />
          {modalState === 1 &&
            <><Box p={3}>   
              <Formik 
                initialValues={{
                  id: '-1',
                  brand: '-1',
                  color: '',
                  code: '',
                  description: '',
                  color_m: '-1',
                  own: false,
                  flag: true
                }}
                validationSchema={Yup.object().shape({
                  brand: Yup.mixed().notOneOf(['-1'], 'Este campo es requerido.'),
                  color: Yup.string().max(20, 'Debe tener 20 caracteres como máximo').required('Se requiere una unidad de medida.'),
                  code: Yup.string().max(20, 'Debe tener 20 caracteres como máximo').required('Se requiere una unidad de medida.'),
                  description: Yup.string().max(50, 'Debe tener 50 caracteres como máximo').required('Se requiere una unidad de medida.'),
                  color_m: Yup.mixed().notOneOf(['-1'], 'Este campo es requerido.'),
                })}
                onSubmit={(values, {resetForm}) => {
                  saveSettings({saving: true});
                  window.setTimeout(() => {
                    saveColor(values).then(res => {
                      saveSettings({saving: false});
                      setDefaultData();
                      resetForm();
                      enqueueSnackbar('Tus datos se han guardado exitosamente.', {
                        variant: 'success'
                      });
                    }).catch(err => {
                      
                      setDefaultData();
                      enqueueSnackbar('No se pudo guardar.', {
                        variant: 'error'
                      });
                      saveSettings({saving: false});
                    });
                  }, 1000);                
                }}
              >
                {({ errors,
                      handleBlur,
                      handleChange,
                      handleSubmit,
                      isSubmitting,
                      setFieldTouched,
                      setFieldValue,
                      touched,
                      values }) => (
                  <form onSubmit={handleSubmit}>
                    <Grid container spacing={3}>
                      <Grid item lg={12} sm={12} xs={12}>
                        <TextField
                          size="small"
                          label={<label>Marca <span style={{color: 'red'}}>*</span></label>}
                          name="brand"
                          error={Boolean(touched.brand && errors.brand)}
                          helperText={touched.brand && errors.brand}
                          fullWidth
                          SelectProps={{ native: true }}
                          select
                          onBlur={handleBlur}
                          onChange={(e) => {
                            handleChange(e);
                          }}
                          value={values.brand}
                          variant="outlined"
                          InputLabelProps={{
                              shrink: true
                          }}
                          >
                          <option key="-1" value="-1">{'-- Seleccionar --'}</option>
                          {brands.map((brand) => (
                              <option
                              key={brand.marca_c_vid}
                              value={brand.marca_c_vid}
                              >
                              {brand.marca_c_vdescripcion}
                              </option>
                          ))}
                        </TextField>
                      </Grid>
                      <Grid item lg={12} sm={12} xs={12}>
                        <TextField
                            size="small"
                            fullWidth
                            label={<label>Color <span style={{color: 'red'}}>*</span></label>}
                            name="color"
                            onBlur={handleBlur}
                            onChange={handleChange} 
                            error={Boolean(touched.color && errors.color)}
                            helperText={touched.color && errors.color}
                            value={values.color}
                            variant="outlined"
                            InputLabelProps={{
                              shrink: true
                            }}
                        />                       
                      </Grid>
                      <Grid item lg={12} sm={12} xs={12}>
                        <TextField
                            size="small"
                            fullWidth
                            label={<label>Código <span style={{color: 'red'}}>*</span></label>}
                            name="code"
                            onBlur={handleBlur}
                            onChange={handleChange} 
                            error={Boolean(touched.code && errors.code)}
                            helperText={touched.code && errors.code}
                            value={values.code}
                            variant="outlined"
                            InputLabelProps={{
                              shrink: true
                            }}
                        />                       
                      </Grid>
                      <Grid item lg={12} sm={12} xs={12}>
                        <TextField
                            size="small"
                            fullWidth
                            label={<label>Descripción <span style={{color: 'red'}}>*</span></label>}
                            name="description"
                            onBlur={handleBlur}
                            onChange={handleChange} 
                            error={Boolean(touched.description && errors.description)}
                            helperText={touched.description && errors.description}
                            value={values.description}
                            variant="outlined"
                            InputLabelProps={{
                              shrink: true
                            }}
                        />                       
                      </Grid>
                      <Grid item lg={12} sm={12} xs={12}>
                        <TextField
                          size="small"
                          label={<label>Color de marca <span style={{color: 'red'}}>*</span></label>}
                          name="color_m"
                          error={Boolean(touched.color_m && errors.color_m)}
                          helperText={touched.color_m && errors.color_m}
                          fullWidth
                          SelectProps={{ native: true }}
                          select
                          onBlur={handleBlur}
                          onChange={(e) => {
                            handleChange(e);
                          }}
                          value={values.color_m}
                          variant="outlined"
                          InputLabelProps={{
                              shrink: true
                          }}
                          >
                          <option key="-1" value="-1">{'-- Seleccionar --'}</option>
                          {originColors.map((color) => (
                              <option
                              key={color.color_c_vid}
                              value={color.color_c_vid}
                              >
                              {color.color_c_vdescripcion}
                              </option>
                          ))}
                        </TextField>                 
                      </Grid> 
                      <Grid item lg={12} sm={12} xs={12} style={{display: 'flex'}}>  
                        <FormControlLabel
                          control={
                            <Switch
                              onChange={handleChange}
                              name="own"
                              value={values.own}
                              checked={values.own}
                              color="primary"
                            />
                          }
                          label={<label>Propio <span style={{color: 'red'}}>*</span></label>}                          
                        />  
                        <Button type="submit" size="small" color="secondary" startIcon={<SaveIcon3 />} variant="contained">
                          GUARDAR
                        </Button>   
                      </Grid>
                    </Grid>
                  </form>   
                )}
              </Formik>
            </Box>
            <Divider /></>
          }
          {modalState === 2 &&
            <><Box p={3}>   
              <Formik 
                initialValues={{
                  id: '-1',
                  brand: '-1',
                  category: '',
                  material: '',
                  process: '',
                  category_m: '-1',
                }}
                validationSchema={Yup.object().shape({
                  brand: Yup.mixed().notOneOf(['-1'], 'Este campo es requerido.'),
                  category: Yup.string().max(20, 'Debe tener 20 caracteres como máximo').required('Se requiere una unidad de medida.'),
                  material: Yup.string().max(100, 'Debe tener 100 caracteres como máximo').required('Se requiere una unidad de medida.'),
                  process: Yup.string().max(100, 'Debe tener 100 caracteres como máximo').required('Se requiere una unidad de medida.'),
                  category_m: Yup.mixed().notOneOf(['-1'], 'Este campo es requerido.'),
                })}
                onSubmit={(values, {resetForm}) => {
                  saveSettings({saving: true});
                  window.setTimeout(() => {
                    saveCategory(values).then(res => {
                      saveSettings({saving: false});
                      setDefaultData();
                      resetForm();
                      enqueueSnackbar('Tus datos se han guardado exitosamente.', {
                        variant: 'success'
                      });
                    }).catch(err => {
                      
                      setDefaultData();
                      enqueueSnackbar('No se pudo guardar.', {
                        variant: 'error'
                      });
                      saveSettings({saving: false});
                    });
                  }, 1000);                
                }}
              >
                {({ errors,
                      handleBlur,
                      handleChange,
                      handleSubmit,
                      isSubmitting,
                      setFieldTouched,
                      setFieldValue,
                      touched,
                      values }) => (
                  <form onSubmit={handleSubmit}>
                    <Grid container spacing={3}>
                      <Grid item lg={12} sm={12} xs={12}>
                        <TextField
                          size="small"
                          label={<label>Marca <span style={{color: 'red'}}>*</span></label>}
                          name="brand"
                          error={Boolean(touched.brand && errors.brand)}
                          helperText={touched.brand && errors.brand}
                          fullWidth
                          SelectProps={{ native: true }}
                          select
                          onBlur={handleBlur}
                          onChange={(e) => {
                            handleChange(e);
                          }}
                          value={values.brand}
                          variant="outlined"
                          InputLabelProps={{
                              shrink: true
                          }}
                          >
                          <option key="-1" value="-1">{'-- Seleccionar --'}</option>
                          {brands.map((brand) => (
                              <option
                              key={brand.marca_c_vid}
                              value={brand.marca_c_vid}
                              >
                              {brand.marca_c_vdescripcion}
                              </option>
                          ))}
                        </TextField>
                      </Grid>
                      <Grid item lg={12} sm={12} xs={12}>
                        <TextField
                            size="small"
                            fullWidth
                            label={<label>Categoría de marca <span style={{color: 'red'}}>*</span></label>}
                            name="category"
                            onBlur={handleBlur}
                            onChange={handleChange} 
                            error={Boolean(touched.category && errors.category)}
                            helperText={touched.category && errors.category}
                            value={values.category}
                            variant="outlined"
                            InputLabelProps={{
                              shrink: true
                            }}
                        />                       
                      </Grid>
                      <Grid item lg={12} sm={12} xs={12}>
                        <TextField
                            size="small"
                            fullWidth
                            label={<label>Material <span style={{color: 'red'}}>*</span></label>}
                            name="material"
                            onBlur={handleBlur}
                            onChange={handleChange} 
                            error={Boolean(touched.material && errors.material)}
                            helperText={touched.material && errors.material}
                            value={values.material}
                            variant="outlined"
                            InputLabelProps={{
                              shrink: true
                            }}
                        />                       
                      </Grid>
                      <Grid item lg={12} sm={12} xs={12}>
                        <TextField
                            size="small"
                            fullWidth
                            label={<label>Proceso <span style={{color: 'red'}}>*</span></label>}
                            name="process"
                            onBlur={handleBlur}
                            onChange={handleChange} 
                            error={Boolean(touched.process && errors.process)}
                            helperText={touched.process && errors.process}
                            value={values.process}
                            variant="outlined"
                            InputLabelProps={{
                              shrink: true
                            }}
                        />                       
                      </Grid>
                      <Grid item lg={12} sm={12} xs={12}>
                        <TextField
                          size="small"
                          label={<label>Categoría <span style={{color: 'red'}}>*</span></label>}
                          name="category_m"
                          error={Boolean(touched.category_m && errors.category_m)}
                          helperText={touched.category_m && errors.category_m}
                          fullWidth
                          SelectProps={{ native: true }}
                          select
                          onBlur={handleBlur}
                          onChange={(e) => {
                            handleChange(e);
                          }}
                          value={values.category_m}
                          variant="outlined"
                          InputLabelProps={{
                              shrink: true
                          }}
                          >
                          <option key="-1" value="-1">{'-- Seleccionar --'}</option>
                          {originCategories.map((category) => (
                              <option
                              key={category.categoria_c_vid}
                              value={category.categoria_c_vid}
                              >
                              {category.categoria_c_vdescripcion}
                              </option>
                          ))}
                        </TextField>                 
                      </Grid> 
                      <Grid item lg={12} sm={12} xs={12} style={{display: 'flex'}}>  
                        <Button type="submit" size="small" color="secondary" startIcon={<SaveIcon3 />} variant="contained">
                          GUARDAR
                        </Button>   
                      </Grid>
                    </Grid>
                  </form>   
                )}
              </Formik>
            </Box>
            <Divider /></>
          }

          <Box
            p={2}
            display="flex"
            alignItems="center"
          >
            <Box flexGrow={1} />
            <Button onClick={onCancel}>
              {'Cancelar'}
            </Button>
          </Box>
        </>
  );
};

export default NewCategory;
