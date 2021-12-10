import { useEffect, useState } from 'react';
import type { FC } from 'react';
import PropTypes from 'prop-types';

import _ from 'lodash';
import * as Yup from 'yup';
import { Formik } from 'formik';
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
    Dialog
} from '@material-ui/core';
import AddIcon2 from '@material-ui/icons/Add';
import type { Theme } from 'src/theme';
import type { Event } from 'src/types/calendar';
import { saveProfile } from 'src/apis/userApi';
import { useSnackbar } from 'notistack';
import useSettings from 'src/hooks/useSettings';
import { isConstructorDeclaration } from 'typescript';
import { ExpandLessSharp } from '@material-ui/icons';
import CheckboxTree from 'react-checkbox-tree';
import 'react-checkbox-tree/lib/react-checkbox-tree.css';


interface NewProfileProps {
    profileid?: Number,
    profiledata?: any[],
    initialnodes?: any[],
    checkedValues?: any[],
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
        marginLeft: theme.spacing(2)
    }
}));

const NewProfile: FC<NewProfileProps> = ({
    profileid,
    profiledata,
    initialnodes,
    checkedValues,
    event,
    _getInitialData,
    onAddComplete,
    onCancel,
    onDeleteComplete,
    onEditComplete
}) => {

    const classes = useStyles();
    const { enqueueSnackbar } = useSnackbar();
    const { saveSettings } = useSettings();
    const [isModalOpen3, setIsModalOpen3] = useState(false);
    const [gridvisible, setGridvisible] = useState<string>('none');
    const [checked, setChecked] = useState<any[]>([]);
    const [expanded, setExpanded] = useState<any[]>([]);


    const [profile, setProfile] = useState<any>([]);

    const handleModalClose3 = (): void => {
        setIsModalOpen3(false);
    };

    const handleModalOpen3 = (): void => {
        setIsModalOpen3(true);
    };


    const getInitialValues = () => {
        if (profileid > -1) {
            return _.merge({}, {
                id: 0,
                profile: "",
                description: "",
                estado: 1,
                method: 1,
                submit: null
            }, {
                id: profileid,
                profile: profiledata[0]?.profile,
                description: profiledata[0]?.description,
                estado: profiledata[0]?.estado,
                method: 1,
                submit: null
            });
        } else {
            return {
                id: 0,
                profile: "",
                description: "",
                estado: 1,
                method: 0,
                submit: null
            };
        }
    };

    const estadoOptions = [{
        value: 1,
        label: "Activo"
    },
    {
        value: 0,
        label: "Inactivo"
    }]

    useEffect(() => {
        if (profileid > -1) {
            setGridvisible('block');
        } else {
            setGridvisible('none');
        }
    }, [profileid])

    useEffect(() => {
        setChecked(checkedValues);
    }, [checkedValues])

    return (
        <>
            <Formik
                initialValues={getInitialValues()}
                onSubmit={async (values, {
                    resetForm,
                    setErrors,
                    setStatus,
                    setSubmitting
                }) => {
                    saveSettings({ saving: true });
                    window.setTimeout(() => {
                        values["checkvalues"] = checked
                        saveProfile(values).then(res => {
                            saveSettings({ saving: false });
                            enqueueSnackbar('Tus datos se han guardado exitosamente.', {
                                variant: 'success'
                            });
                            onCancel();
                        }).catch(err => {
                            enqueueSnackbar('No se pudo guardar.', {
                                variant: 'error'
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
                    values
                }) => (
                    <form onSubmit={handleSubmit}>
                        <Box p={3}>
                            <Typography
                                align="center"
                                gutterBottom
                                variant="h4"
                                color="textPrimary"
                            >
                                {profileid > -1 ? 'ACCESOS POR PERFIL' : 'Nuevo PERFIL'}
                            </Typography>
                        </Box>
                        <Divider />
                        <Box p={3}>
                            <Grid container spacing={3}>
                                <Grid item lg={12} sm={12} xs={12}>
                                    <TextField
                                        size="small"
                                        fullWidth
                                        label={<label>Perfil <span style={{ color: 'red' }}>*</span></label>}
                                        name="profile"
                                        onBlur={handleBlur}
                                        onChange={handleChange}
                                        value={values.profile}
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
                                        label={<label>Descripción <span style={{ color: 'red' }}>*</span></label>}
                                        name="description"
                                        onBlur={handleBlur}
                                        onChange={handleChange}
                                        value={values.description}
                                        variant="outlined"
                                        InputLabelProps={{
                                            shrink: true
                                        }}
                                    />
                                </Grid>
                                <Grid item lg={12} sm={12} xs={12}>
                                    <CheckboxTree
                                        nodes={initialnodes}
                                        checked={checked}
                                        showNodeIcon={false}
                                        expanded={expanded}
                                        onCheck={checked => setChecked(checked)}
                                        onExpand={expanded => setExpanded(expanded)}
                                    />
                                </Grid>

                                <Grid item lg={12} sm={12} xs={12} >
                                    <TextField
                                        size="small"
                                        fullWidth
                                        SelectProps={{ native: true }}
                                        select
                                        label={<label>Estado</label>}
                                        name="estado"
                                        onBlur={handleBlur}
                                        onChange={handleChange}
                                        value={values.estado}
                                        variant="outlined"
                                        InputLabelProps={{
                                            shrink: true
                                        }}
                                    >
                                        {estadoOptions.map((option) => (
                                            <option
                                                key={option.value}
                                                value={option.value}
                                            >
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
                                <FormHelperText error>
                                    {errors.submit}
                                </FormHelperText>
                            </Box>
                        )}
                        <Box
                            p={2}
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
        </>
    );
};

NewProfile.propTypes = {
    // @ts-ignore
    event: PropTypes.object,
    onAddComplete: PropTypes.func,
    onCancel: PropTypes.func,
    onDeleteComplete: PropTypes.func,
    onEditComplete: PropTypes.func,
    // @ts-ignore
    range: PropTypes.object
};

export default NewProfile;
