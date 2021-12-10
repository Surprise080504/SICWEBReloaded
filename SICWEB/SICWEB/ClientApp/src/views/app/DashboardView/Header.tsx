import React, { useRef, useState } from "react";
import type { FC } from "react";
import { Link as RouterLink } from "react-router-dom";
import PropTypes from "prop-types";
import clsx from "clsx";
import {
  Breadcrumbs,
  Button,
  Grid,
  Link,
  Menu,
  MenuItem,
  SvgIcon,
  Typography,
  makeStyles,
} from "@material-ui/core";
import NavigateNextIcon from "@material-ui/icons/NavigateNext";
import { Calendar as CalendarIcon } from "react-feather";

interface HeaderProps {
  className?: string;
}

const timeRanges = [
  {
    value: "today",
    text: "Hoy",
  },
  {
    value: "yesterday",
    text: "Ayer",
  },
  {
    value: "last_30_days",
    text: "Últimos 30 días",
  },
  {
    value: "last_year",
    text: "El año pasado",
  },
];

const useStyles = makeStyles(() => ({
  root: {},
}));

const Header: FC<HeaderProps> = ({ className, ...rest }) => {
  const classes = useStyles();
  const actionRef = useRef<any>(null);
  const [isMenuOpen, setMenuOpen] = useState<boolean>(false);
  const [timeRange, setTimeRange] = useState<string>(timeRanges[2].text);

  return (
    <Grid
      container
      spacing={3}
      justifyContent="space-between"
      className={clsx(classes.root, className)}
      {...rest}
    >
      <Grid item>
        <Breadcrumbs
          separator={<NavigateNextIcon fontSize="small" />}
          aria-label="breadcrumb"
        >
          <Link variant="body1" color="inherit" to="/" component={RouterLink}>
            Tablero
          </Link>
          <Typography variant="body1" color="textPrimary">
            Informe
          </Typography>
        </Breadcrumbs>
        <Typography variant="h3" color="textPrimary">
          Esto es lo que esta pasando
        </Typography>
      </Grid>
      <Grid item>
        <Button
          ref={actionRef}
          onClick={() => setMenuOpen(true)}
          startIcon={
            <SvgIcon fontSize="small">
              <CalendarIcon />
            </SvgIcon>
          }
        >
          {timeRange}
        </Button>
        <Menu
          anchorEl={actionRef.current}
          onClose={() => setMenuOpen(false)}
          open={isMenuOpen}
          getContentAnchorEl={null}
          anchorOrigin={{
            vertical: "bottom",
            horizontal: "center",
          }}
          transformOrigin={{
            vertical: "top",
            horizontal: "center",
          }}
        >
          {timeRanges.map((_timeRange) => (
            <MenuItem
              key={_timeRange.value}
              onClick={() => setTimeRange(_timeRange.text)}
            >
              {_timeRange.text}
            </MenuItem>
          ))}
        </Menu>
      </Grid>
    </Grid>
  );
};

Header.propTypes = {
  className: PropTypes.string,
};

export default Header;
