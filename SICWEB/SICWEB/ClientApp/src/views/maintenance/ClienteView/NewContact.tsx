import { FC, useEffect, useState } from 'react';
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
  Grid,
  TextareaAutosize,
  makeStyles
} from '@material-ui/core';
import { useSnackbar } from 'notistack';
import SaveIcon3 from '@material-ui/icons/Save';
import useSettings from 'src/hooks/useSettings';
import { getCargo, saveContact } from 'src/apis/clienteApi';
import { KeyboardDatePicker } from '@material-ui/pickers';

import { getFamilies1, getSubFamilies, saveFamily, saveSubFamily, saveUnit } from 'src/apis/itemApi';
import type { Theme } from 'src/theme';


const useStyles = makeStyles((theme: Theme) => ({
    root: {},
    confirmButton: {
      marginLeft: theme.spacing(2)
    },
    shippableField: {
      marginLeft: theme.spacing(2)
    },
}));

interface NewContactProps {
    modalState?: any;
    cargo?: any;
    addContact: (any) => void;
    onCancel?: () => void;
}

const NewContact: FC<NewContactProps> = ({
  modalState,
  cargo,
  addContact,
  onCancel,
}) => {
  const { enqueueSnackbar } = useSnackbar();
  const { saveSettings } = useSettings();
  
  const classes = useStyles();

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
                modalState === 0 && 'NUEVO CONTACTO'
              }
            </Typography>
          </Box>
          <Divider />
          {modalState === 0 &&
          <><Box p={3}>   
            <Formik 
              initialValues={{
                id: '-1',
                type: '1',
                identification: '',
                name: '',
                surname: '',
                lastname: '',
                landline: '',
                phone: '',
                email: '',
                birthday: null,
                cargo: 1,
                observations: '',                
                flag: true
              }}
              validationSchema={Yup.object().shape({
                identification: Yup.string().max(12, 'Debe tener 12 caracteres como máximo').matches(/^[0123456789]+$/, 'documento de identificación incorrecto'),
                landline: Yup.string().max(15, 'Debe tener 15 caracteres como máximo').matches(/^((\\+[1-9]{1,4}[ \\-]*)|(\\([0-9]{2,3}\\)[ \\-]*)|([0-9]{2,4})[ \\-]*)*?[0-9]{3,4}?[ \\-]*[0-9]{3,4}?$/, 'Número de teléfono incorrecto'),
                phone: Yup.string().max(15, 'Debe tener 15 caracteres como máximo').matches(/^((\\+[1-9]{1,4}[ \\-]*)|(\\([0-9]{2,3}\\)[ \\-]*)|([0-9]{2,4})[ \\-]*)*?[0-9]{3,4}?[ \\-]*[0-9]{3,4}?$/, 'Número de teléfono incorrecto'),
                name: Yup.string().max(50, 'Debe tener 50 caracteres como máximo').required('Se requiere el tipo de Nombre'),
                surname: Yup.string().max(50, 'Debe tener 50 caracteres como máximo').required('Se requiere el tipo de Apellido Paterno'),
                lastname: Yup.string().max(50, 'Debe tener 50 caracteres como máximo'),
                observations: Yup.string().max(50, 'Debe tener 50 caracteres como máximo'),
                email: Yup.string().max(50, 'Debe tener 50 caracteres como máximo').email('email incorrecto.').required('Se requiere el tipo de email'),
              })}
              onSubmit={(values, {resetForm}) => {
                saveSettings({saving: true});
                window.setTimeout(() => {
                    addContact(values);
                    saveSettings({saving: false});
                    onCancel();
                    // saveContact(values).then(res => {
                    //     saveSettings({saving: false});
                    //     resetForm();
                    //     enqueueSnackbar('Tus datos se han guardado exitosamente.', {
                    //     variant: 'success'
                    //     });
                    //     onCancel();
                    // }).catch(err => {
                    //     enqueueSnackbar('No se pudo guardar.', {
                    //     variant: 'error'
                    //     });
                    //     saveSettings({saving: false});
                    // });
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
                        <Grid item xl={6} lg={6} md={6} sm={6} xs={12}>
                            <TextField
                                size="small"
                                label={<label>Tipo de Documento</label>}
                                name="type"
                                fullWidth
                                select
                                SelectProps={{ native: true }}
                                onBlur={handleBlur}
                                onChange={(e) => {
                                    handleChange(e);
                                }}
                                value={values.type}
                                variant="outlined"
                                InputLabelProps={{
                                    shrink: true
                                }}
                            >
                                <option
                                    key={1}
                                    value={1}
                                    >
                                    {'DNI'}
                                </option>
                                <option
                                    key={2}
                                    value={2}
                                    >
                                    {'CARNE EXTRANJERIA'}
                                </option>
                            </TextField>                                        
                        </Grid>                                
                        <Grid item xl={6} lg={6} md={6} sm={6} xs={12}>
                            <TextField
                                size="small"
                                error={Boolean(touched.identification && errors.identification)}
                                fullWidth
                                helperText={touched.identification && errors.identification}
                                label={<label>Documento de Identidad</label>}
                                InputLabelProps={{
                                    shrink: true
                                }}
                                InputProps={{
                                    className: 'maskInputNumber'
                                }}
                                name="identification"
                                onBlur={handleBlur}
                                onChange={handleChange}
                                value={values.identification}
                                variant="outlined"
                            />                                   
                        </Grid> 
                        <Grid item xl={6} lg={6} md={6} sm={6} xs={12}>
                            <TextField
                                size="small"
                                error={Boolean(touched.name && errors.name)}
                                fullWidth
                                helperText={touched.name && errors.name}
                                label={<label>Nombre <span style={{color: 'red'}}>*</span></label>}
                                InputLabelProps={{
                                    shrink: true
                                }}
                                name="name"
                                onBlur={handleBlur}
                                onChange={handleChange}
                                value={values.name}
                                variant="outlined"
                            />                                   
                        </Grid> 
                        <Grid item xl={6} lg={6} md={6} sm={6} xs={12}>
                            <TextField
                                size="small"
                                error={Boolean(touched.surname && errors.surname)}
                                fullWidth
                                helperText={touched.surname && errors.surname}
                                label={<label>Apellido Paterno <span style={{color: 'red'}}>*</span></label>}
                                InputLabelProps={{
                                    shrink: true
                                }}
                                name="surname"
                                onBlur={handleBlur}
                                onChange={handleChange}
                                value={values.surname}
                                variant="outlined"
                            />                                   
                        </Grid> 
                        <Grid item xl={6} lg={6} md={6} sm={6} xs={12}>
                            <TextField
                                size="small"
                                error={Boolean(touched.lastname && errors.lastname)}
                                fullWidth
                                helperText={touched.lastname && errors.lastname}
                                label={<label>Apellido Materno</label>}
                                InputLabelProps={{
                                    shrink: true
                                }}
                                name="lastname"
                                onBlur={handleBlur}
                                onChange={handleChange}
                                value={values.lastname}
                                variant="outlined"
                            />                                   
                        </Grid> 
                        <Grid item xl={6} lg={6} md={6} sm={6} xs={12}>
                            <TextField
                                size="small"
                                error={Boolean(touched.landline && errors.landline)}
                                fullWidth
                                helperText={touched.landline && errors.landline}
                                label={<label>Teléfono Fijo</label>}
                                InputLabelProps={{
                                    shrink: true
                                }}
                                name="landline"
                                onBlur={handleBlur}
                                onChange={handleChange}
                                value={values.landline}
                                variant="outlined"
                                InputProps={{
                                    className: 'maskInputNumber'
                                }}
                            />                                   
                        </Grid> 
                        <Grid item xl={6} lg={6} md={6} sm={6} xs={12}>
                            <TextField
                                size="small"
                                error={Boolean(touched.phone && errors.phone)}
                                fullWidth
                                helperText={touched.phone && errors.phone}
                                label={<label>Teléfono Móvil</label>}
                                InputLabelProps={{
                                    shrink: true
                                }}
                                name="phone"
                                onBlur={handleBlur}
                                onChange={handleChange}
                                value={values.phone}
                                variant="outlined"
                                InputProps={{
                                    className: 'maskInputNumber'
                                }}
                            />                                   
                        </Grid> 
                        <Grid item xl={6} lg={6} md={6} sm={6} xs={12}>
                            <TextField
                                size="small"
                                error={Boolean(touched.email && errors.email)}
                                fullWidth
                                helperText={touched.email && errors.email}
                                label={<label>E-mail <span style={{color: 'red'}}>*</span></label>}
                                InputLabelProps={{
                                    shrink: true
                                }}
                                type="email"
                                name="email"
                                onBlur={handleBlur}
                                onChange={handleChange}
                                value={values.email}
                                variant="outlined"
                            />                                   
                        </Grid>                             
                            
                        <Grid item xl={6} lg={6} md={6} sm={6} xs={12}>
                            <KeyboardDatePicker
                                size="small"
                                fullWidth
                                format="DD/MM/yyyy"
                                inputVariant="outlined"
                                label={<label>Fecha de Nacimiento</label>}
                                name="birthday"
                                onClick={() => setFieldTouched('birthday')}
                                onChange={(date) => setFieldValue('birthday', date)}
                                value={values.birthday}
                                KeyboardButtonProps={{
                                    'aria-label': 'change date',

                                }}
                                cancelLabel={'Cancelar'}
                                okLabel={'OK'}
                                InputLabelProps={{
                                    shrink: true
                                }}
                            />                                 
                        </Grid>   
                        <Grid item xl={6} lg={6} md={6} sm={6} xs={12}>
                            <TextField
                                size="small"
                                label={<label>Cargo <span style={{color: 'red'}}>*</span></label>}
                                name="cargo"
                                error={Boolean(touched.cargo && errors.cargo)}
                                helperText={touched.cargo && errors.cargo && 'Se requiere el tipo de persona'}
                                fullWidth
                                select
                                SelectProps={{ native: true }}
                                onBlur={handleBlur}
                                onChange={(e) => {
                                    handleChange(e);
                                }}
                                value={values.cargo}
                                variant="outlined"
                                InputLabelProps={{
                                    shrink: true
                                }}
                            >
                                {cargo.map((_cargo) => (
                                    <option
                                    key={_cargo.cli_contac_cargo_c_yid}
                                    value={_cargo.cli_contac_cargo_c_yid}
                                    >
                                    {_cargo.cli_contac_cargo_c_vnomb}
                                    </option>
                                ))}
                            </TextField>                                        
                        </Grid> 
                        <Grid  item xl={12} xs={12}>
                            <label style={{fontSize: 12.5, color: 'rgba(0, 0, 0, 0.54)'}}>&nbsp;&nbsp;&nbsp;&nbsp;Nombre Comercial</label>
                        </Grid>
                        <Grid item xl={12} lg={12} md={12} sm={12} xs={12}>
                            <TextareaAutosize 
                                style={{
                                    resize: 'vertical',
                                    width: 'calc(100% - 20px)',
                                    padding: 10,
                                    borderColor: Boolean(errors.observations) ? 'red' : 'rgba(0, 0, 0, 0.12)',
                                    borderRadius: 4,
                                    minHeight: 50
                                }}
                                name="observations"
                                onChange={(e) => setFieldValue('observations', e.target.value)}
                                value={values.observations}
                            aria-label="minimum height" placeholder="Minimum 3 rows" />   
                            {Boolean(errors.observations) && 
                            <label style={{fontSize: 12.5, color: 'red'}}>{errors.observations}</label>                                   
                            }
                        </Grid>                              
                    </Grid>
                    
                    <Divider />
                    <Box
                        p={0}
                        display="flex"
                        alignItems="center"
                        >
                        <Box flexGrow={1} />
                        <Button onClick={onCancel}>
                            {'Cancelar'}
                        </Button>
                        <Button
                            variant="contained"
                            type="submit"
                            disabled={isSubmitting}
                            color="secondary"
                            className={classes.confirmButton} 
                            >
                            {'Confirmar'}
                        </Button>
                    </Box>
                </form>   
              )}
            </Formik>
          </Box>
          </>
          }

          
        </>
  );
};

export default NewContact;
