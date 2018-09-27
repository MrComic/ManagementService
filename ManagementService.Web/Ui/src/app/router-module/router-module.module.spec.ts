import { RouterModuleModule } from './router-module.module';

describe('RouterModuleModule', () => {
  let routerModuleModule: RouterModuleModule;

  beforeEach(() => {
    routerModuleModule = new RouterModuleModule();
  });

  it('should create an instance', () => {
    expect(routerModuleModule).toBeTruthy();
  });
});
