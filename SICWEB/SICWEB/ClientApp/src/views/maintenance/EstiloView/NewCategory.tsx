import { FC, useEffect, useState } from "react";
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
  Dialog,
  IconButton,
  Grid,
} from "@material-ui/core";
import AddIcon2 from "@material-ui/icons/Add";
import { useSnackbar } from "notistack";
import SaveIcon3 from "@material-ui/icons/Save";
import useSettings from "src/hooks/useSettings";
import { ColorPicker } from "material-ui-color";
import {
  saveCategory,
  saveColor,
  saveMarca,
  getOriginCategories,
} from "src/apis/styleApi";
import NewCategoryDetail from "./NewCategoryDetail";
import getColorName from "hex-to-color-name";

interface NewCategoryProps {
  modalState?: any;
  brands?: any[];
  originColors?: any[];
  event?: Event;
  setDefaultData?: () => void;
  onCancel?: () => void;
}

const NewCategory: FC<NewCategoryProps> = ({
  modalState,
  brands,
  originColors,
  setDefaultData,
  onCancel,
}) => {
  const { enqueueSnackbar } = useSnackbar();
  const { saveSettings } = useSettings();

  const [isModalOpen3, setIsModalOpen3] = useState(false);
  const [originCategories, setOriginCategories] = useState<any>([]);
  const [marcaColor, setMarcaColor] = useState<string>("");

  const [scolor_m, setSColor_m] = useState<string>("");
  const [sown, setSOwn] = useState<boolean>(false);

  const [basecolor, setbasecolor] = useState<boolean>(true);

  const colours = {
    aliceblue: "#f0f8ff",
    antiquewhite: "#faebd7",
    aqua: "#00ffff",
    aquamarine: "#7fffd4",
    azure: "#f0ffff",
    beige: "#f5f5dc",
    bisque: "#ffe4c4",
    black: "#000000",
    blanchedalmond: "#ffebcd",
    blue: "#0000ff",
    blueviolet: "#8a2be2",
    brown: "#a52a2a",
    burlywood: "#deb887",
    cadetblue: "#5f9ea0",
    chartreuse: "#7fff00",
    chocolate: "#d2691e",
    coral: "#ff7f50",
    cornflowerblue: "#6495ed",
    cornsilk: "#fff8dc",
    crimson: "#dc143c",
    cyan: "#00ffff",
    darkblue: "#00008b",
    darkcyan: "#008b8b",
    darkgoldenrod: "#b8860b",
    darkgray: "#a9a9a9",
    darkgreen: "#006400",
    darkkhaki: "#bdb76b",
    darkmagenta: "#8b008b",
    darkolivegreen: "#556b2f",
    darkorange: "#ff8c00",
    darkorchid: "#9932cc",
    darkred: "#8b0000",
    darksalmon: "#e9967a",
    darkseagreen: "#8fbc8f",
    darkslateblue: "#483d8b",
    darkslategray: "#2f4f4f",
    darkturquoise: "#00ced1",
    darkviolet: "#9400d3",
    deeppink: "#ff1493",
    deepskyblue: "#00bfff",
    dimgray: "#696969",
    dodgerblue: "#1e90ff",
    firebrick: "#b22222",
    floralwhite: "#fffaf0",
    forestgreen: "#228b22",
    fuchsia: "#ff00ff",
    gainsboro: "#dcdcdc",
    ghostwhite: "#f8f8ff",
    gold: "#ffd700",
    goldenrod: "#daa520",
    gray: "#808080",
    green: "#008000",
    greenyellow: "#adff2f",
    honeydew: "#f0fff0",
    hotpink: "#ff69b4",
    "indianred ": "#cd5c5c",
    indigo: "#4b0082",
    ivory: "#fffff0",
    khaki: "#f0e68c",
    lavender: "#e6e6fa",
    lavenderblush: "#fff0f5",
    lawngreen: "#7cfc00",
    lemonchiffon: "#fffacd",
    lightblue: "#add8e6",
    lightcoral: "#f08080",
    lightcyan: "#e0ffff",
    lightgoldenrodyellow: "#fafad2",
    lightgrey: "#d3d3d3",
    lightgreen: "#90ee90",
    lightpink: "#ffb6c1",
    lightsalmon: "#ffa07a",
    lightseagreen: "#20b2aa",
    lightskyblue: "#87cefa",
    lightslategray: "#778899",
    lightsteelblue: "#b0c4de",
    lightyellow: "#ffffe0",
    lime: "#00ff00",
    limegreen: "#32cd32",
    linen: "#faf0e6",
    magenta: "#ff00ff",
    maroon: "#800000",
    mediumaquamarine: "#66cdaa",
    mediumblue: "#0000cd",
    mediumorchid: "#ba55d3",
    mediumpurple: "#9370d8",
    mediumseagreen: "#3cb371",
    mediumslateblue: "#7b68ee",
    mediumspringgreen: "#00fa9a",
    mediumturquoise: "#48d1cc",
    mediumvioletred: "#c71585",
    midnightblue: "#191970",
    mintcream: "#f5fffa",
    mistyrose: "#ffe4e1",
    moccasin: "#ffe4b5",
    navajowhite: "#ffdead",
    navy: "#000080",
    oldlace: "#fdf5e6",
    olive: "#808000",
    olivedrab: "#6b8e23",
    orange: "#ffa500",
    orangered: "#ff4500",
    orchid: "#da70d6",
    palegoldenrod: "#eee8aa",
    palegreen: "#98fb98",
    paleturquoise: "#afeeee",
    palevioletred: "#d87093",
    papayawhip: "#ffefd5",
    peachpuff: "#ffdab9",
    peru: "#cd853f",
    pink: "#ffc0cb",
    plum: "#dda0dd",
    powderblue: "#b0e0e6",
    purple: "#800080",
    rebeccapurple: "#663399",
    red: "#ff0000",
    rosybrown: "#bc8f8f",
    royalblue: "#4169e1",
    saddlebrown: "#8b4513",
    salmon: "#fa8072",
    sandybrown: "#f4a460",
    seagreen: "#2e8b57",
    seashell: "#fff5ee",
    sienna: "#a0522d",
    silver: "#c0c0c0",
    skyblue: "#87ceeb",
    slateblue: "#6a5acd",
    slategray: "#708090",
    snow: "#fffafa",
    springgreen: "#00ff7f",
    steelblue: "#4682b4",
    tan: "#d2b48c",
    teal: "#008080",
    thistle: "#d8bfd8",
    tomato: "#ff6347",
    turquoise: "#40e0d0",
    violet: "#ee82ee",
    wheat: "#f5deb3",
    white: "#ffffff",
    whitesmoke: "#f5f5f5",
    yellow: "#ffff00",
    yellowgreen: "#9acd32",
  };

  const handleModalClose3 = (): void => {
    setIsModalOpen3(false);
  };

  const handleModalOpen3 = (): void => {
    setIsModalOpen3(true);
  };

  const handleChangeSwitch = (e) => {
    setSOwn(e.target.checked);
    if (!e.target.checked) {
      setSColor_m(marcaColor);
    } else {
      setSColor_m("");
    }
  };

  const handleChangeColor_m = (e) => {
    setSColor_m(e.target.value);
  };

  useEffect(() => {
    getOriginCategories()
      .then((res) => {
        setOriginCategories(res);
      })
      .catch((err) => {
        setOriginCategories([]);
      });

    setSColor_m(marcaColor);
  }, []);

  useEffect(() => {
    getOriginCategories()
      .then((res) => {
        setOriginCategories(res);
      })
      .catch((err) => {
        setOriginCategories([]);
      });
  }, [isModalOpen3]);

  return (
    <>
      <Box p={3}>
        <Typography
          align="center"
          gutterBottom
          variant="h4"
          color="textPrimary"
        >
          {modalState === 0 && "Marca"}
          {modalState === 1 && "Color de Marca"}
          {modalState === 2 && "Categoría de Marca"}
        </Typography>
      </Box>
      <Divider />
      {modalState === 0 && (
        <>
          <Box p={3}>
            <Formik
              initialValues={{
                marca: "",
                description: "",
              }}
              validationSchema={Yup.object().shape({
                marca: Yup.string()
                  .max(20, "Debe tener 20 caracteres como máximo")
                  .required("Se requiere un nombre o código de marca."),
                description: Yup.string()
                  .max(50, "Debe tener 50 caracteres como máximo")
                  .required("Se requiere una descripción de marca."),
              })}
              onSubmit={(values, { resetForm }) => {
                saveSettings({ saving: true });
                window.setTimeout(() => {
                  saveMarca(values)
                    .then((res) => {
                      saveSettings({ saving: false });
                      enqueueSnackbar(
                        "Tus datos se han guardado exitosamente.",
                        {
                          variant: "success",
                        }
                      );
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
                            Marca<span style={{ color: "red" }}>*</span>
                          </label>
                        }
                        name="marca"
                        onBlur={handleBlur}
                        onChange={handleChange}
                        error={Boolean(touched.marca && errors.marca)}
                        helperText={touched.marca && errors.marca}
                        value={values.marca}
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
                        name="description"
                        onBlur={handleBlur}
                        onChange={handleChange}
                        error={Boolean(
                          touched.description && errors.description
                        )}
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
      )}
      {modalState === 1 && (
        <>
          <Box p={3}>
            <Formik
              initialValues={{
                id: "-1",
                brand: "-1",
                color: "",
                code: "",
                description: "",
                color_m: "",
                own: false,
                flag: true,
              }}
              validationSchema={Yup.object().shape({
                brand: Yup.mixed().notOneOf(["-1"], "Este campo es requerido."),
                code: Yup.string()
                  .max(20, "Debe tener 20 caracteres como máximo")
                  .required("Se requiere una unidad de medida."),
                description: Yup.string()
                  .max(50, "Debe tener 50 caracteres como máximo")
                  .required("Se requiere una unidad de medida."),

                //color_m: Yup.mixed().notOneOf(['-1'], 'Este campo es requerido.'),
                //color_m:Yup.string().max(50, 'Este campo es requerido.').required('Se requiere una unidad de medida.'),
              })}
              onSubmit={(values, { resetForm }) => {
                values["own"] = sown;
                if (basecolor) {
                  if (typeof colours[marcaColor.toLowerCase()] != "undefined") {
                    values["color"] = colours[marcaColor.toLowerCase()];
                    values["colorName"] = marcaColor;
                    if (!sown) {
                      values["color_m"] = colours[marcaColor.toLowerCase()];
                    } else {
                      values["color_m"] = scolor_m;
                    }
                  } else {
                    values["color"] = ""; //"#ffffff";
                    values["colorName"] = "white";
                    if (!sown) {
                      values["color_m"] = "#ffffff";
                    } else {
                      values["color_m"] = scolor_m;
                    }
                  }
                } else {
                  values["color"] = marcaColor;
                  values["colorName"] = getColorName(marcaColor);
                  if (!sown) {
                    values["color_m"] = marcaColor;
                  } else {
                    values["color_m"] = scolor_m;
                  }
                }
                saveSettings({ saving: true });
                if (values["color"] == "") {
                  enqueueSnackbar("No se pudo guardar.", {
                    variant: "error",
                  });
                  saveSettings({ saving: false });
                  return;
                }
                window.setTimeout(() => {
                  saveColor(values)
                    .then((res) => {
                      saveSettings({ saving: false });

                      setDefaultData();
                      resetForm();
                      enqueueSnackbar(
                        "Tus datos se han guardado exitosamente.",
                        {
                          variant: "success",
                        }
                      );

                      onCancel();
                    })
                    .catch((err) => {
                      setDefaultData();
                      enqueueSnackbar("No se pudo guardar: " + err.message, {
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
                        label={
                          <label>
                            Marca <span style={{ color: "red" }}>*</span>
                          </label>
                        }
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
                          shrink: true,
                        }}
                      >
                        <option key="-1" value="-1">
                          {"-- Seleccionar --"}
                        </option>
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
                    <Grid
                      item
                      lg={12}
                      sm={12}
                      xs={12}
                      style={{
                        display: "flex",
                        alignItems: "center",
                        gap: "50px",
                      }}
                    >
                      {/* <TextField
                      size="small"
                      fullWidth
                      label={<label>Color <span style={{ color: 'red' }}>*</span></label>}
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
                    /> */}
                      <label>
                        Color <span style={{ color: "red" }}>*</span>
                      </label>
                      <ColorPicker
                        defaultValue=""
                        onChange={(e) => {
                          if (typeof e == "string") {
                            setMarcaColor(e);
                            if (!sown) {
                              setSColor_m(e);
                            }
                            setbasecolor(true);
                            // setMarcaColor(getColorName(e));
                          } else {
                            setMarcaColor(e.css.backgroundColor);
                            if (!sown) {
                              setSColor_m(e.css.backgroundColor);
                            }
                            setbasecolor(false);
                            // setMarcaColor(getColorName(e.css.backgroundColor));
                          }
                        }}
                        value={marcaColor}
                      />
                    </Grid>
                    <Grid item lg={12} sm={12} xs={12}>
                      <TextField
                        size="small"
                        fullWidth
                        label={
                          <label>
                            Código <span style={{ color: "red" }}>*</span>
                          </label>
                        }
                        name="code"
                        onBlur={handleBlur}
                        onChange={handleChange}
                        error={Boolean(touched.code && errors.code)}
                        helperText={touched.code && errors.code}
                        value={values.code}
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
                            Descripción <span style={{ color: "red" }}>*</span>
                          </label>
                        }
                        name="description"
                        onBlur={handleBlur}
                        onChange={handleChange}
                        error={Boolean(
                          touched.description && errors.description
                        )}
                        helperText={touched.description && errors.description}
                        value={values.description}
                        variant="outlined"
                        InputLabelProps={{
                          shrink: true,
                        }}
                      />
                    </Grid>
                    <Grid item lg={12} sm={12} xs={12}>
                      {/* <TextField
                      size="small"
                      label={<label>Color de marca <span style={{ color: 'red' }}>*</span></label>}
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
                    </TextField> */}
                      <TextField
                        size="small"
                        fullWidth
                        label={
                          <label>
                            Color de marca{" "}
                            <span style={{ color: "red" }}>*</span>
                          </label>
                        }
                        name="color_m"
                        onBlur={handleBlur}
                        onChange={handleChangeColor_m}
                        error={Boolean(touched.color_m && errors.color_m)}
                        helperText={touched.color_m && errors.color_m}
                        value={scolor_m}
                        disabled={sown ? false : true}
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
                      <FormControlLabel
                        control={
                          <Switch
                            onChange={handleChangeSwitch}
                            name="own"
                            value={sown}
                            checked={sown}
                            color="primary"
                          />
                        }
                        label={
                          <label>
                            Propio <span style={{ color: "red" }}>*</span>
                          </label>
                        }
                      />
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
      )}
      {modalState === 2 && (
        <>
          <Box p={3}>
            <Formik
              initialValues={{
                id: "-1",
                brand: "-1",
                category: "",
                material: "",
                process: "",
                category_m: "-1",
              }}
              validationSchema={Yup.object().shape({
                brand: Yup.mixed().notOneOf(["-1"], "Este campo es requerido."),
                category: Yup.string()
                  .max(20, "Debe tener 20 caracteres como máximo")
                  .required("Se requiere una unidad de medida."),
                material: Yup.string()
                  .max(100, "Debe tener 100 caracteres como máximo")
                  .required("Se requiere una unidad de medida."),
                process: Yup.string()
                  .max(100, "Debe tener 100 caracteres como máximo")
                  .required("Se requiere una unidad de medida."),
                category_m: Yup.mixed().notOneOf(
                  ["-1"],
                  "Este campo es requerido."
                ),
              })}
              onSubmit={(values, { resetForm }) => {
                saveSettings({ saving: true });
                window.setTimeout(() => {
                  saveCategory(values)
                    .then((res) => {
                      saveSettings({ saving: false });
                      setDefaultData();
                      resetForm();
                      enqueueSnackbar(
                        "Tus datos se han guardado exitosamente.",
                        {
                          variant: "success",
                        }
                      );
                    })
                    .catch((err) => {
                      setDefaultData();
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
                        label={
                          <label>
                            Marca <span style={{ color: "red" }}>*</span>
                          </label>
                        }
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
                          shrink: true,
                        }}
                      >
                        <option key="-1" value="-1">
                          {"-- Seleccionar --"}
                        </option>
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
                        label={
                          <label>
                            Categoría de marca{" "}
                            <span style={{ color: "red" }}>*</span>
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
                            Material <span style={{ color: "red" }}>*</span>
                          </label>
                        }
                        name="material"
                        onBlur={handleBlur}
                        onChange={handleChange}
                        error={Boolean(touched.material && errors.material)}
                        helperText={touched.material && errors.material}
                        value={values.material}
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
                            Proceso <span style={{ color: "red" }}>*</span>
                          </label>
                        }
                        name="process"
                        onBlur={handleBlur}
                        onChange={handleChange}
                        error={Boolean(touched.process && errors.process)}
                        helperText={touched.process && errors.process}
                        value={values.process}
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
                        label={
                          <label>
                            Categoría <span style={{ color: "red" }}>*</span>
                          </label>
                        }
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
                          shrink: true,
                        }}
                      >
                        <option key="-1" value="-1">
                          {"-- Seleccionar --"}
                        </option>
                        {originCategories.map((category) => (
                          <option
                            key={category.categoria_c_vid}
                            value={category.categoria_c_vid}
                          >
                            {category.categoria_c_vdescripcion}
                          </option>
                        ))}
                      </TextField>
                      <IconButton
                        size="small"
                        color="secondary"
                        aria-label="add to shopping cart"
                        onClick={() => {
                          handleModalOpen3();
                        }}
                      >
                        <AddIcon2 />
                      </IconButton>
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
            <Dialog
              maxWidth="xs"
              fullWidth
              onClose={handleModalClose3}
              open={isModalOpen3}
            >
              {isModalOpen3 && (
                <NewCategoryDetail onCancel={handleModalClose3} />
              )}
            </Dialog>
          </Box>
          <Divider />
        </>
      )}

      <Box p={2} display="flex" alignItems="center">
        <Box flexGrow={1} />
        <Button onClick={onCancel}>{"Cancelar"}</Button>
      </Box>
    </>
  );
};

export default NewCategory;
