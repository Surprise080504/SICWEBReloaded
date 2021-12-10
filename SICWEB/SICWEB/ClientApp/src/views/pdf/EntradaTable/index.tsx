import React from "react";
import type { FC } from "react";
import PropTypes from "prop-types";

import MetaTableHeader from "./MetaTableHeader";
import MetaTableRow from "./MetaTableRow";

import {
  Page,
  Document,
  StyleSheet,
  View,
} from "@react-pdf/renderer";

interface EntradaMetaPDFProps {
  data?: any;
  className?: string;
}

const styles = StyleSheet.create({
  page: {
    fontFamily: "Helvetica",
    fontSize: 11,
    paddingTop: 30,
    paddingLeft: 20,
    paddingRight: 20,
    lineHeight: 1.5,
    flexDirection: "column",
  },
  tableContainer: {
    flexDirection: "row",
    flexWrap: "wrap",
    marginTop: 24,
    borderWidth: 1,
    borderColor: "#000000",
  },
});

const EntradaMetaPDF: FC<EntradaMetaPDFProps> = ({ data }) => {
  return (
    <Document>
      <Page size="A4" style={styles.page}>
        <View style={styles.tableContainer}>
          <MetaTableHeader />
          <MetaTableRow items={data} />
        </View>
      </Page>
    </Document>
  );
};

EntradaMetaPDF.propTypes = {
  data: PropTypes.any,
};

EntradaMetaPDF.defaultProps = {};
export default EntradaMetaPDF;
