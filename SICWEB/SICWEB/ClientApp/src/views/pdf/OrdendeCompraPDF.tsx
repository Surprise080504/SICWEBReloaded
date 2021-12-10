import React from "react";
import type { FC } from "react";
import PropTypes from "prop-types";

import {
  Page,
  Document,
  Image,
  StyleSheet,
  View,
  Text,
} from "@react-pdf/renderer";

interface OrdendeCompraPDFProps {
  data?: any;
  className?: string;
}

const styles = StyleSheet.create({
  page: {
    fontFamily: "Helvetica",
    fontSize: 11,
    paddingTop: 30,
    paddingLeft: 60,
    paddingRight: 60,
    lineHeight: 1.5,
    flexDirection: "column",
  },
  section: {
    margin: 10,
    padding: 10,
    flexGrow: 1,
    textAlign: "center",
  },
});

const OrdendeCompraPDF: FC<OrdendeCompraPDFProps> = ({ data }) => (
  <Document>
    <Page size="A4" style={styles.page}>
      <View style={styles.section}>
        <Text>
          ORDEN DE COMPRA #{data.detail.odc_c_cserie}{" "}
          {data.detail.odc_c_vcodigo}
        </Text>
      </View>
    </Page>
  </Document>
);

OrdendeCompraPDF.propTypes = {
  data: PropTypes.any,
};

OrdendeCompraPDF.defaultProps = {};
export default OrdendeCompraPDF;
