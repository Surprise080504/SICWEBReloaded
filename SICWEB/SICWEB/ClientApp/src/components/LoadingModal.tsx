import {
  FC} from 'react';
import {
  Dialog, makeStyles
} from '@material-ui/core';

import clsx from 'clsx';
import CircularProgress from '@material-ui/core/CircularProgress';
import { green } from '@material-ui/core/colors';
import Fab from '@material-ui/core/Fab';
import SaveIcon from '@material-ui/icons/Save';

interface LoadingModalProps {
  isModalOpen?: boolean;
  handleModalClose?: () => void;
}

const useStyles = makeStyles((theme) => ({
  root: {
    display: 'flex',
    alignItems: 'center',
    width: 72
  },
  wrapper: {
    margin: theme.spacing(1),
    position: 'relative',
    width: 56
  },
  buttonSuccess: {
    backgroundColor: green[500],
    '&:hover': {
      backgroundColor: green[700],
    },
  },
  fabProgress: {
    color: green[500],
    position: 'absolute',
    top: -6,
    left: -6,
    zIndex: 1,
  },
  buttonProgress: {
    color: green[500],
    position: 'absolute',
    top: '50%',
    left: '50%',
    marginTop: -12,
    marginLeft: -12,
  },
}));

const LoadingModal: FC<LoadingModalProps> = ({ isModalOpen, handleModalClose, }) => {
  const classes = useStyles();

  // const [loading, setLoading] = useState(false);
  // const [success, setSuccess] = useState(false);
  // const timer = useRef(null);

  const buttonClassname = clsx({
      [classes.buttonSuccess]: false,//success
  });

  // useEffect(() => {
  //   return () => {
  //     clearTimeout(timer.current);
  //   };
  // }, []);
  
  // const handleButtonClick = () => {
  //   if (!loading) {
  //     setSuccess(false);
  //     setLoading(true);
  //     timer.current = window.setTimeout(() => {
  //       setSuccess(true);
  //       setLoading(false);
  //     }, 2000);
  //   }
  // };


  return (
    <Dialog
        maxWidth="sm"
        fullWidth
        onClose={handleModalClose}
        open={isModalOpen}
        PaperProps={{
          style: {
            width: 100,
            height: 100,
            alignItems: 'center',
            justifyContent: 'center'
          }
        }}
      >
        {
          isModalOpen && (
            <div className={classes.root}>
              <div className={classes.wrapper}>
                <Fab
                  aria-label="save"
                  color="primary"
                  className={buttonClassname}
                  // onClick={handleButtonClick}
                >
                  {/* {success ? <CheckIcon /> : <SaveIcon />} */}
                  <SaveIcon />
                </Fab>
                {/* {(loading || true) && <CircularProgress size={68} className={classes.fabProgress} />} */}
                {<CircularProgress size={68} className={classes.fabProgress} />}
              </div>
            </div>
          )
        }
    </Dialog>
  );
};

LoadingModal.propTypes = {
};

export default LoadingModal;
