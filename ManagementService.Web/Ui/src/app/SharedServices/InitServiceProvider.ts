import {InitService} from './init.service';

export function InitServiceProvider(config: InitService) {
  return () => config.loadDefaultData();
}
