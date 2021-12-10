import { FC, ReactNode, useEffect, useState } from 'react';
import PropTypes from 'prop-types';
import { makeStyles } from '@material-ui/core';
import useSettings from 'src/hooks/useSettings';

import TopBar from './TopBar';
import LoadingModal from 'src/components/LoadingModal';

interface DashboardLayoutProps {
  children?: ReactNode
}

const useStyles = makeStyles((theme) => ({
  root: {
    backgroundColor: theme.palette.background.default,
    display: 'flex',
    height: '100%',
    overflow: 'hidden',
    width: '100%'
  },
  wrapper: {
    display: 'flex',
    flex: '1 1 auto',
    overflow: 'hidden',
    paddingTop: 128
  },
  [theme.breakpoints.down(583)]: {
    wrapper: {
      paddingTop: 168
    }
  },
  contentContainer: {
    display: 'flex',
    flex: '1 1 auto',
    overflow: 'hidden'
  },
  content: {
    flex: '1 1 auto',
    height: '100%',
    overflow: 'auto'
  }
}));

const DashboardLayout: FC<DashboardLayoutProps> = ({ children }) => {

  const classes = useStyles();
  const { settings } = useSettings();

  const [isSaveModalOpen, setIsSaveModalOpen] = useState(false);

  useEffect(() => {    
    setIsSaveModalOpen(settings.saving);
  }, [settings])
  return (
    <div className={classes.root}>
      <TopBar />
      <div className={classes.wrapper}>
        <div className={classes.contentContainer}>
          <div className={classes.content}>
            {children}
          </div>
        </div>
      </div>
      
      <LoadingModal 
        isModalOpen={isSaveModalOpen}
      />
    </div>
  );
};

DashboardLayout.propTypes = {
  children: PropTypes.node
};

export default DashboardLayout;
