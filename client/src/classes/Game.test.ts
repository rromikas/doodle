import * as Game from "./Game";

// @ponicode
describe("changeSpeed", () => {
  let inst: any;

  beforeEach(() => {
    inst = new Game.Game();
  });

  test("0", () => {
    let result: any = inst.changeSpeed(true);
    expect(result).toMatchSnapshot();
  });

  test("1", () => {
    let result: any = inst.changeSpeed(false);
    expect(result).toMatchSnapshot();
  });

  test("2", () => {
    let result: any = inst.changeSpeed(null);
    expect(result).toMatchSnapshot();
  });
});
