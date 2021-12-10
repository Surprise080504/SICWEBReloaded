import type { FC } from 'react';
import {
  Container,
  makeStyles
} from '@material-ui/core';
import Page from 'src/components/Page';
import type { Theme } from 'src/theme';
import Header from './Header';
import OCTable from './OCTable';


const useStyles = makeStyles((theme: Theme) => ({
  root: {
    backgroundColor: theme.palette.background.default
  }
}));

const OCView: FC = () => {
  
  const classes = useStyles();

  const products = [];
  return (
    <Page
      className={classes.root}
      title="Settings"
    >
      <Container maxWidth={false}>
        <Header />
        <OCTable/>
      </Container>
    </Page>
  );
};
export default OCView;