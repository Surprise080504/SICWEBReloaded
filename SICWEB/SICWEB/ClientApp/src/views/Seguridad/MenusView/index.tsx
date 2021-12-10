import type { FC } from "react";
import { Container, makeStyles } from "@material-ui/core";
import Page from "src/components/Page";
import type { Theme } from "src/theme";
import Header from "./Header";
import Tables from "./Tables";

const useStyles = makeStyles((theme: Theme) => ({
  root: {
    backgroundColor: theme.palette.background.default,
  },
}));

const DashboardView: FC = (props) => {
  const classes = useStyles();
  const menus = [];
  return (
    <Page className={classes.root} title="Settings">
      <Container maxWidth={false}>
        <Header />
        <Tables products={menus} />
      </Container>
    </Page>
  );
};
export default DashboardView;
