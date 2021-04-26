import { PropsWithChildren } from 'react';

import styles from './Layout.module.scss';

const Layout = ({ children }: PropsWithChildren<{}>) => {
  return (
    <div className={styles.wrapper}>
      <div className={styles.content}>{children}</div>
    </div>
  );
};

export default Layout;
