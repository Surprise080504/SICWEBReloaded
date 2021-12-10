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
  serie: {
    width: "7%",
    fontSize: "8px",
    fontStyle: "bold",
  },
  codigo: {
    width: "10%",
    fontSize: "8px",
    fontStyle: "bold",
  },
  ruc: {
    width: "15%",
    fontSize: "8px",
    fontStyle: "bold",
  },
  proveedor: {
    width: "33%",
    fontSize: "8px",
    fontStyle: "bold",
  },
  estado: {
    width: "15%",
    fontSize: "8px",
    fontStyle: "bold",
  },
  moneda: {
    width: "10%",
    fontSize: "8px",
    fontStyle: "bold",
  },
  montototal: {
    width: "10%",
    fontSize: "8px",
    fontStyle: "bold",
  },
});

const MetaTableHeader = () => (
  <View style={styles.container}>
    <Text style={styles.serie}>SERIE</Text>
    <Text style={styles.codigo}>CÓDIGO</Text>
    <Text style={styles.ruc}>RUC PROVEEDOR</Text>
    <Text style={styles.proveedor}>PROVEEDOR</Text>
    <Text style={styles.estado}>ESTADO</Text>
    <Text style={styles.moneda}>MONEDA</Text>
    <Text style={styles.montototal}> MONTO TOTAL</Text>
  </View>
);

export default MetaTableHeader;
