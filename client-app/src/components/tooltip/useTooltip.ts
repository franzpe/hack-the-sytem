import { DependencyList, useEffect } from 'react';
import ReactTooltip from 'react-tooltip';

export const useTooltip = (deps?: DependencyList) => {
  useEffect(() => {
    ReactTooltip.rebuild();
  }, deps);
};
