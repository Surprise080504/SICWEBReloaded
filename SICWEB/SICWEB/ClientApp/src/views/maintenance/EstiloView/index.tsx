import type { FC } from "react";
import { Container, makeStyles } from "@material-ui/core";
import Page from "src/components/Page";
import type { Theme } from "src/theme";
import Header from "./Header";
import EstiloTable from "./EstiloTable";

const useStyles = makeStyles((theme: Theme) => ({
  root: {
    backgroundColor: theme.palette.background.default,
  },
}));

const EstiloView: FC = () => {
  const classes = useStyles();

  return (
    <Page className={classes.root} title="Settings">
      <Container maxWidth={false}>
        <Header />
        <EstiloTable />
      </Container>
    </Page>
  );
};
export default EstiloView;
