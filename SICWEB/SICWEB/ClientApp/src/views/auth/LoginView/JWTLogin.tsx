import React from 'react';
import type { FC } from 'react';
import clsx from 'clsx';
import * as Yup from 'yup';
import PropTypes from 'prop-types';
import { Formik } from 'formik';
import {
    Box,
    Button,
    FormHelperText,
    TextField,
    makeStyles,
    InputAdornment,
    InputLabel
} from '@material-ui/core';
import { Alert } from '@material-ui/lab';
import useAuth from 'src/hooks/useAuth';
import useIsMountedRef from 'src/hooks/useIsMountedRef';
import { useSnackbar } from 'notistack';

interface JWTLoginProps {
    className?: string;
}

const useStyles = makeStyles(() => ({
    root: {}
}));

const JWTLogin: FC<JWTLoginProps> = ({ className, ...rest }) => {
    const classes = useStyles();
    const { login } = useAuth() as any;
    const isMountedRef = useIsMountedRef();
    const { enqueueSnackbar } = useSnackbar();

    return (
        <Formik
            initialValues={{
                email: 'Ingrese su usuario',
                password: '1234',
                submit: null
            }}
            validationSchema={Yup.object().shape({
                email: Yup.string().max(255).required('Se requiere nombre de usuario'),
                password: Yup.string().max(255).required('se requiere contraseña')
            })}
            onSubmit={async (values, {
                setErrors,
                setStatus,
                setSubmitting
            }) => {
                try {
                    const status = await login(values.email, values.password);

                    status === 0 && enqueueSnackbar('Usuario y/o Password incorrectos', {
                        variant: 'error'
                    });

                    status === -1 && enqueueSnackbar('El usuario actual está inactivo.', {
                        variant: 'error'
                    });
                    if (isMountedRef.current) {
                        setStatus({ success: true });
                        setSubmitting(false);
                    } else {
                    }
                } catch (err) {
                    console.error(err);
                    if (isMountedRef.current) {
                        setStatus({ success: false });
                        setErrors({ submit: err.message });
                        setSubmitting(false);
                    }
                }
            }}
        >
            {({
                errors,
                handleBlur,
                handleChange,
                handleSubmit,
                isSubmitting,
                touched,
                values
            }) => (
                <form
                    noValidate
                    onSubmit={handleSubmit}
                    className={clsx(classes.root, className)}
                    {...rest}
                >
                    <TextField
                        error={Boolean(touched.email && errors.email)}
                        fullWidth
                        autoFocus
                        helperText={touched.email && errors.email}
                        label={<label>Nombre de usuario <span style={{ color: 'red' }}>*</span></label>}
                        InputLabelProps={{
                            shrink: true
                        }}
                        placeholder="Ingrese su usuario"
                        margin="normal"
                        name="email"
                        onBlur={handleBlur}
                        onChange={handleChange}
                        type="email"
                        value={values.email}
                        variant="outlined"
                    />
                    <TextField
                        error={Boolean(touched.password && errors.password)}
                        fullWidth
                        helperText={touched.password && errors.password}
                        label={<label>Contraseña <span style={{ color: 'red' }}>*</span></label>}
                        InputLabelProps={{
                            shrink: true
                        }}
                        margin="normal"
                        placeholder="Ingrese su contraseña"
                        name="password"
                        onBlur={handleBlur}
                        onChange={handleChange}
                        type="password"
                        value={values.password}
                        variant="outlined"
                    />
                    {errors.submit && (
                        <Box mt={3}>
                            <FormHelperText error>
                                {errors.submit}
                            </FormHelperText>
                        </Box>
                    )}
                    <Box mt={2}>
                        <Button
                            color="secondary"
                            disabled={isSubmitting}
                            fullWidth
                            size="large"
                            type="submit"
                            variant="contained"
                        >
                            iniciar sesión
                        </Button>
                    </Box>
                    {/*<Box mt={2}>*/}
                    {/*  <Alert*/}
                    {/*    severity="info"*/}
                    {/*  >*/}
                    {/*    <div>*/}
                    {/*      Utilice */}
                    {/*      {' '}*/}
                    {/*      <b>ADMIN</b>*/}
                    {/*      {' '}*/}
                    {/*      y contraseña */}
                    {/*      {' '}*/}
                    {/*      <b>juan899833245</b>*/}
                    {/*    </div>*/}
                    {/*  </Alert>*/}
                    {/*</Box>*/}
                </form>
            )}
        </Formik>
    );
};

JWTLogin.propTypes = {
    className: PropTypes.string,
};

export default JWTLogin;
