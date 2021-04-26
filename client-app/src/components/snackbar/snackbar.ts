/* eslint-disable */
import Snackbar from 'node-snackbar';
import cx from 'classnames';
import 'node-snackbar/dist/snackbar.min.css';

import styles from './snackbar.module.scss';

/**
 *
 * @param {SnackbarOptions} options
 */
export const showSnackbar = (text: string, options?: SnackbarOptions) =>
  Snackbar.show({
    text,
    duration: 4000,
    actionText: 'dismiss',
    pos: 'bottom-left',
    customClass: styles.container,
    ...options
  });

export const showErrorSnackbar = (text: string, options?: SnackbarOptions) => {
  Snackbar.show({
    text,
    duration: 4000,
    actionText: 'dismiss',
    pos: 'bottom-left',
    customClass: cx(styles.container, styles.error),
    ...options
  });
};
