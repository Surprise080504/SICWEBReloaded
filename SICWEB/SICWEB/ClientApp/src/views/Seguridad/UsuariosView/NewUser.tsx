import { useEffect, useState } from "react";
import type { FC } from "react";
import PropTypes from "prop-types";

import _ from "lodash";
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
import NewProfile from "./NewProfile";
import type { Theme } from "src/theme";
import type { Event } from "src/types/calendar";
import {
  saveUser,
  getProfiles,
  getProfile,
  getAccessProfile,
  getCheckedValues,
  getCheckedcrudValues,
  checkImageFile,
} from "src/apis/userApi";
import { useSnackbar } from "notistack";
import useSettings from "src/hooks/useSettings";
import { isConstructorDeclaration } from "typescript";
import { LaptopWindows } from "@material-ui/icons";

interface NewUserProps {
  editID: number;
  _initialValue?: any;
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
  container: {
    display: "flex",
    flexDirection: "column",
    justifyContent: "center",
    alignItems: "center",
  },
  preview: {
    marginTop: 20,
    display: "flex",
    flexDirection: "column",
  },
  image: { width: 200, height: 200 },
  delete: {
    cursor: "pointer",
    padding: 15,
    background: "red",
    color: "white",
    border: "none",
  },
  selectedImageInput: {
    display: "none",
  },
  selectedImageButton: {
    display: "block",
    width: 200,
    height: 30,
  },
}));

const NewUser: FC<NewUserProps> = ({
  editID,
  _initialValue,
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
  const [profiles, setProfiles] = useState<any>([]);
  const [userInfo, setuserInfo] = useState<any>({});
  const [profiledata, setProfileData] = useState<any>({});
  const [checkedValues, setCheckedValues] = useState<any>([]);

  const [modalState, setModalState] = useState(0);
  const [profileid, setProfileid] = useState<any>(-1);

  const [nodes, setNodes] = useState<any>([]);

  const handleModalClose3 = (): void => {
    setIsModalOpen3(false);
    _getProfiles();
  };

  const handleModalOpen3 = (): void => {
    setIsModalOpen3(true);
  };

  const handleModalClose = (): void => {
    setIsModalOpen3(false);
  };

  const handleImageUpload = (): void => {
    document.getElementById("getFile").click();
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

  const getInitialValues = () => {
    if (editID > -1) {
      return _.merge(
        {},
        {
          name: "",
          lastname: "",
          mlastname: "",
          user: "",
          password: "",
          networkuser: "",
          profile_id: -1,
          estado: 1,
          submit: null,
        },
        {
          name: _initialValue[editID].name,
          lastname: _initialValue[editID].lastname,
          mlastname: _initialValue[editID].mlastname,
          user: _initialValue[editID].user,
          password: _initialValue[editID].password,
          networkuser: _initialValue[editID].networkuser,
          profile_id: _initialValue[editID].profile_id,
          estado: _initialValue[editID].estado,
          submit: null,
        }
      );
    } else {
      return {
        name: "",
        lastname: "",
        mlastname: "",
        user: "",
        password: "",
        networkuser: "",
        profile_id: -1,
        estado: 1,
        submit: null,
      };
    }
  };

  const _getProfiles = () => {
    getProfiles().then((res) => {
      setProfiles(res);
    });
  };

  const makeTree = (items, parent = 0) => {
    var tempList = [];
    let subItems = items.filter((i: any) => i?.parentid === parent);
    let otherItems = items.filter((i: any) => i?.parentid !== parent);
    subItems.map((item) => {
      let _subItems = otherItems.filter((i: any) => i?.parentid === item.value);
      let _children = [];
      if (_subItems.length) {
        _children = makeTree(otherItems, item.value);
      } else {
        if (parent != 0) {
          _children = [
            { value: item.value + "-a", label: "Nuevo" },
            { value: item.value + "-b", label: "Editar" },
            { value: item.value + "-c", label: "Ver" },
            { value: item.value + "-d", label: "Eliminar" },
            { value: item.value + "-e", label: "Procesar" },
            { value: item.value + "-f", label: "Imprimir" },
          ];
        } else {
          _children = null;
        }
        subItems.map((childnode) => {
          if (childnode.value == item.value && childnode.childvalue != 0) {
            if (_children != null) {
              _children.push({
                value: childnode.value + "-" + childnode.childvalue,
                label: childnode.childlabel,
              });
            }
          }
        });
      }

      let data = {
        value: item.value,
        label: item.label,
        children: _children,
      };

      var isExist = false;
      tempList.map((item, index) => {
        if (data.value == item.value) {
          isExist = true;
        }
      });

      if (!isExist) {
        tempList.push(data);
      }
    });
    return tempList;
  };

  const _getAccessProfile = (profileid) => {
    getAccessProfile(profileid).then((res: []) => {
      window.setTimeout(() => {
        var output = makeTree(res);
        setNodes(output);
      }, 100);
    });
  };

  const handleChange = (e: any): void => {
    if (e.target.name === "profile_id") {
      setProfileid(e.target.value);
      _getProfile(e.target.value);
    }

    var data = { [e.target.name]: e.target.value };

    setuserInfo({ ...userInfo, ...data });
  };

  const _getCheckedValues = (profileid) => {
    var tmpCheckedValue = [];
    getCheckedValues(profileid).then((res: any) => {
      res.map((data: any) => {
        tmpCheckedValue.push(data.menuid + "-" + data.menuopcionid);
      });
    });

    getCheckedcrudValues(profileid).then((res: any) => {
      res.map((data: any) => {
        if (data.a == "A") {
          tmpCheckedValue.push(data.menuid + "-a");
        }
        if (data.b == "A") {
          tmpCheckedValue.push(data.menuid + "-b");
        }
        if (data.c == "A") {
          tmpCheckedValue.push(data.menuid + "-c");
        }
        if (data.d == "A") {
          tmpCheckedValue.push(data.menuid + "-d");
        }
        if (data.e == "A") {
          tmpCheckedValue.push(data.menuid + "-e");
        }
        if (data.f == "A") {
          tmpCheckedValue.push(data.menuid + "-f");
        }
      });
    });

    setCheckedValues(tmpCheckedValue);
  };

  const [selectedImage, setSelectedImage] = useState<any>();

  const [selectedImageUrl, setSelectedImageUrl] = useState<string>();

  useEffect(() => {
    _getProfile(profileid);
    _getAccessProfile(profileid);
    _getCheckedValues(profileid);
  }, [profileid, isModalOpen3]);

  useEffect(() => {
    _getProfiles();
    setProfileid(_initialValue[editID]?.profile_id);
    _getProfile(_initialValue[editID]?.profile_id);
    _getAccessProfile(_initialValue[editID]?.profile_id);
    const iniVal = getInitialValues();
    if (iniVal) {
      setuserInfo({
        ...iniVal,
      });
    }

    _getCheckedValues(_initialValue[editID]?.profile_id);
    _checkUserImage();
  }, []);

  const _checkUserImage = async () => {
    const checkImage = await checkImageFile(
      `estilo_image_${_initialValue[editID]?.user}.png`
    );

    if (checkImage) {
      setSelectedImageUrl(
        "/uploads/" + "estilo_image_" + _initialValue[editID]?.user + ".png"
      );
    } else {
      setSelectedImageUrl("/uploads/thumb.png");
    }

    setSelectedImage(true);
  };

  const _getProfile = (profileid) => {
    getProfile(profileid).then((res) => {
      setProfileData(res);
    });
  };

  // This function will be triggered when the file field change
  const imageChange = (e) => {
    if (e.target.files && e.target.files.length > 0) {
      setSelectedImage(e.target.files[0]);
      setSelectedImageUrl(URL.createObjectURL(e.target.files[0]));
    }
  };

  return (
    <>
      <Formik
        initialValues={getInitialValues()}
        onSubmit={async (values, { setSubmitting }) => {
          saveSettings({ saving: true });
          values = { ...values, ...userInfo };
          window.setTimeout(() => {
            values["estado"] = Number(values?.estado) === 1 ? true : false;
            values["profile_id"] = Number(values?.profile_id);
            values["method"] = editID;
            saveUser(values, selectedImage)
              .then((res) => {
                saveSettings({ saving: false });
                _getInitialData();
                enqueueSnackbar("Tus datos se han guardado exitosamente.", {
                  variant: "success",
                });
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
          isSubmitting,
          setFieldTouched,
          setFieldValue,
          touched,
        }) => (
          <form onSubmit={handleSubmit}>
            <Box p={3}>
              <Typography
                align="center"
                gutterBottom
                variant="h4"
                color="textPrimary"
              >
                {editID > -1 ? "Editar Usuario" : "Nuevo Usuario"}
              </Typography>
            </Box>
            <Divider />
            <Box p={3}>
              <Grid container lg={12} sm={12} xs={12}>
                <Grid container item spacing={3} lg={8} sm={12} xs={12}>
                  <Grid item lg={12} sm={12} xs={12}>
                    <TextField
                      size="small"
                      error={Boolean(touched.name && errors.name)}
                      fullWidth
                      helperText={touched.name && errors.name}
                      label={
                        <label>
                          Nombres <span style={{ color: "red" }}>*</span>
                        </label>
                      }
                      name="name"
                      onBlur={handleBlur}
                      onChange={handleChange}
                      value={userInfo.name}
                      variant="outlined"
                      InputLabelProps={{
                        shrink: true,
                      }}
                    />
                  </Grid>

                  <Grid item lg={12} sm={12} xs={12}>
                    <TextField
                      size="small"
                      error={Boolean(touched.lastname && errors.lastname)}
                      fullWidth
                      helperText={touched.lastname && errors.lastname}
                      label={
                        <label>
                          Apellido Paterno{" "}
                          <span style={{ color: "red" }}>*</span>
                        </label>
                      }
                      name="lastname"
                      onBlur={handleBlur}
                      onChange={handleChange}
                      value={userInfo.lastname}
                      variant="outlined"
                      InputLabelProps={{
                        shrink: true,
                      }}
                    />
                  </Grid>

                  <Grid item lg={12} sm={12} xs={12}>
                    <TextField
                      size="small"
                      error={Boolean(touched.mlastname && errors.mlastname)}
                      fullWidth
                      helperText={touched.mlastname && errors.mlastname}
                      label={
                        <label>
                          Apellido Materno{" "}
                          <span style={{ color: "red" }}>*</span>
                        </label>
                      }
                      name="mlastname"
                      onBlur={handleBlur}
                      onChange={handleChange}
                      value={userInfo.mlastname}
                      variant="outlined"
                      InputLabelProps={{
                        shrink: true,
                      }}
                    />
                  </Grid>

                  <Grid item lg={12} sm={12} xs={12}>
                    <TextField
                      size="small"
                      error={Boolean(touched.user && errors.user)}
                      fullWidth
                      helperText={touched.user && errors.user}
                      label={
                        <label>
                          Usuario <span style={{ color: "red" }}>*</span>
                        </label>
                      }
                      name="user"
                      onBlur={handleBlur}
                      onChange={handleChange}
                      disabled={editID > -1 ? true : false}
                      value={userInfo.user}
                      variant="outlined"
                      InputLabelProps={{
                        shrink: true,
                      }}
                    />
                  </Grid>

                  <Grid item lg={12} sm={12} xs={12}>
                    <TextField
                      size="small"
                      error={Boolean(touched.password && errors.password)}
                      fullWidth
                      helperText={touched.password && errors.password}
                      label={
                        <label>
                          Password <span style={{ color: "red" }}>*</span>
                        </label>
                      }
                      name="password"
                      onBlur={handleBlur}
                      onChange={handleChange}
                      value={userInfo.password}
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
                      label={<label>Usuario de red</label>}
                      name="networkuser"
                      onBlur={handleBlur}
                      onChange={handleChange}
                      disabled={true}
                      value={userInfo.networkuser}
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
                    <TextField
                      size="small"
                      error={Boolean(touched.profile_id && errors.profile_id)}
                      fullWidth
                      label={<label>Perfiles</label>}
                      name="profile_id"
                      SelectProps={{ native: true }}
                      select
                      onBlur={handleBlur}
                      onChange={handleChange}
                      value={userInfo.profile_id}
                      variant="outlined"
                      InputLabelProps={{
                        shrink: true,
                      }}
                    >
                      {<option key={-1} value={-1} />}
                      {profiles.map((profile) => (
                        <option
                          key={profile.perf_c_yid}
                          value={profile.perf_c_yid}
                        >
                          {profile.perf_c_vnomb}
                        </option>
                      ))}
                    </TextField>
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
                      onChange={handleChange}
                      value={userInfo.estado}
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
                <Grid container item lg={4} sm={12} xs={12}>
                  <Grid item lg={12} sm={12} xs={12}>
                    <div className={classes.container}>
                      <Button
                        className={classes.selectedImageButton}
                        onClick={handleImageUpload}
                      >
                        Cargar imagen
                      </Button>
                      <input
                        className={classes.selectedImageInput}
                        type="file"
                        id="getFile"
                        onChange={imageChange}
                        accept="image/*"
                      />

                      {selectedImage && (
                        <div
                          className={classes.preview}
                          style={{
                            textAlign: "center",
                          }}
                        >
                          <img
                            src={selectedImageUrl}
                            className={classes.image}
                            style={{
                              width: 250,
                              maxHeight: 250,
                            }}
                            alt="Thumb"
                          />
                        </div>
                      )}
                    </div>
                  </Grid>
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
          <NewProfile
            profileid={profileid}
            profiledata={profiledata}
            initialnodes={nodes}
            checkedValues={checkedValues}
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

NewUser.propTypes = {
  // @ts-ignore
  event: PropTypes.object,
  onAddComplete: PropTypes.func,
  onCancel: PropTypes.func,
  onDeleteComplete: PropTypes.func,
  onEditComplete: PropTypes.func,
  // @ts-ignore
  range: PropTypes.object,
};

export default NewUser;
