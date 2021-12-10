import React, { Fragment } from "react";
import { Text, View, StyleSheet } from "@react-pdf/renderer";

const borderColor = "#000000";
const styles = StyleSheet.create({
  row: {
    flexDirection: "row",
    borderTopColor: "#000000",
    borderTopWidth: 1,
    alignItems: "center",
    height: 24,
    textAlign: "center",
  },
  serie: {
    width: "7%",
    fontSize: "8px",
  },
  codigo: {
    width: "10%",
    fontSize: "8px",
  },
  ruc: {
    width: "15%",
    fontSize: "8px",
  },
  proveedor: {
    width: "33%",
    fontSize: "8px",
  },
  estado: {
    width: "15%",
    fontSize: "8px",
  },
  moneda: {
    width: "10%",
    fontSize: "8px",
  },
  montototal: {
    width: "10%",
    fontSize: "8px",
  },
});

const MetaTableRow = ({ items }) => {
  const rows = items.map((item) => (
    <View style={styles.row} key={item.id}>
      <Text style={styles.serie}>{item.serie}</Text>
      <Text style={styles.codigo}>{item.codigo}</Text>
      <Text style={styles.ruc}>{item.ruc}</Text>
      <Text style={styles.proveedor}>{item.prov}</Text>
      <Text style={styles.estado}>{item.estado}</Text>
      <Text style={styles.moneda}>{item.moneda}</Text>
      <Text style={styles.montototal}>{item.monototal}</Text>
    </View>
  ));
  return <Fragment>{rows}</Fragment>;
};

export default MetaTableRow;
