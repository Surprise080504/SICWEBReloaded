import type { FC } from 'react';
import {
  Container,
  Grid,
  makeStyles
} from '@material-ui/core';
import Page from 'src/components/Page';
import type { Theme } from 'src/theme';
import Header from './Header';
import NewProjects from './NewProjects';
import RoiPerCustomer from './RoiPerCustomer';
import SystemHealth from './SystemHealth';
import TodaysMoney from './TodaysMoney';
import LineChart from './LineChart';

const useStyles = makeStyles((theme: Theme) => ({
  root: {
    backgroundColor: theme.palette.background.default,
    // minHeight: '100%',
    // paddingTop: theme.spacing(3),
    // paddingBottom: theme.spacing(3)
  }
}));

const DashboardView: FC = () => {
  
  const classes = useStyles();

  return (
    <Page
      className={classes.root}
      title="Tablero"
    >
      <Container maxWidth={false}>
        <Header />
        <Grid
          container
          spacing={3}
        >
          <Grid
            item
            lg={3}
            sm={6}
            xs={12}
          >
            <TodaysMoney />
          </Grid>
          <Grid
            item
            lg={3}
            sm={6}
            xs={12}
          >
            <NewProjects />
          </Grid>
          <Grid
            item
            lg={3}
            sm={6}
            xs={12}
          >
            <SystemHealth />
          </Grid>
          <Grid
            item
            lg={3}
            sm={6}
            xs={12}
          >
            <RoiPerCustomer />
          </Grid>          
        </Grid>
        <Grid
          container
          spacing={10}
        >
          <Grid
            item
            lg={12}
            sm={12}
            xs={12}
          >
            <LineChart />
          </Grid>
        </Grid>
      </Container>
    </Page>
  );
};
export default DashboardView;