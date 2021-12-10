import type { FC, ReactNode } from "react";
import { Redirect } from "react-router-dom";
import PropTypes from "prop-types";
import useAuth from "src/hooks/useAuth";

interface AuthGuardProps {
  children?: ReactNode;
}

const AuthGuard: FC<AuthGuardProps> = ({ children }) => {
  const { isAuthenticated } = useAuth();
  if (!isAuthenticated) {
    return <Redirect to="/login" />;
  }

  return <>{children}</>;
};

AuthGuard.propTypes = {
  children: PropTypes.node,
};

export default AuthGuard;
