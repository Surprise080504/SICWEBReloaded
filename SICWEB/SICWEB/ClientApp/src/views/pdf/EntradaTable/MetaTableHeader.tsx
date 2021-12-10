import React from "react";
import { Text, View, StyleSheet } from "@react-pdf/renderer";

const borderColor = "#000000";
const styles = StyleSheet.create({
  container: {
    flexDirection: "row",
    backgroundColor: "#ffffff",
    alignItems: "center",
    height: 24,
    textAlign: "center",
  },
  ruc: {
    width: "10%",
    fontSize: "8px",
    fontStyle: "bold",
  },
  razonsocial: {
    width: "30%",
    fontSize: "8px",
    fontStyle: "bold",
  },
  fecha: {
    width: "17%",
    fontSize: "8px",
    fontStyle: "bold",
  },
  estado: {
    width: "17%",
    fontSize: "8px",
    fontStyle: "bold",
  },
  movid: {
    width: "8%",
    fontSize: "8px",
    fontStyle: "bold",
  },
  odcserie: {
    width: "8%",
    fontSize: "8px",
    fontStyle: "bold",
  },
  odccodigo: {
    width: "10%",
    fontSize: "8px",
    fontStyle: "bold",
  },
});

const MetaTableHeader = () => (
  <View style={styles.container}>
    <Text style={styles.movid}>MOV. ID</Text>
    <Text style={styles.odcserie}>ODC. SERIE</Text>
    <Text style={styles.odccodigo}>ODC. CÓDIGO</Text>
    <Text style={styles.ruc}>RUC PROVEEDOR</Text>
    <Text style={styles.razonsocial}>RAZÓN SOCIAL PROVEEDOR</Text>
    <Text style={styles.fecha}>FECHA REGISTRO</Text>
    <Text style={styles.estado}>ESTADO</Text>
  </View>
);

export default MetaTableHeader;
