import React, { Fragment } from "react";
import { Text, View, StyleSheet } from "@react-pdf/renderer";
import { format } from 'date-fns';

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

const MetaTableRow = ({ items }) => {
  const rows = items.map((item) => (
    <View style={styles.row} key={item.id}>
      <Text style={styles.movid}>{item.id}</Text>
      <Text style={styles.odcserie}>{item.odc_c_cserie}</Text>
      <Text style={styles.odccodigo}>{item.odc_c_vcodigo}</Text>
      <Text style={styles.ruc}>{item.ruc}</Text>
      <Text style={styles.razonsocial}>{item.razonsocial}</Text>
      <Text style={styles.fecha}>{format(new Date(item.fecha), 'MM/dd/yyyy HH:MM:ss')}</Text>
      <Text style={styles.estado}>{item.estado}</Text>
    </View>
  ));
  return <Fragment>{rows}</Fragment>;
};

export default MetaTableRow;
