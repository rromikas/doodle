import BaseUnit from "./BaseUnit";

interface Composite extends BaseUnit {
  items: BaseUnit[];
}

export default Composite;
