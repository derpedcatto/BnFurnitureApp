import { useDispatch, useSelector } from "react-redux";
import { AppDispatch, RootState } from "../../../app/store";
import { 
  decrement, 
  increment,
  incrementByAmount,
  incrementAsync
} from "../counterSlice";

const Counter = () => {
  const count = useSelector((state: RootState) => state.counter.value);
  const dispatch = useDispatch<AppDispatch>();

  return (
    <>
      <h2>{count}</h2>
      <div style={{display: "flex", flexDirection: "column"}}>
        <button onClick={() => dispatch(increment())}>Increment</button>
        <button onClick={() => dispatch(incrementByAmount(10))}>Increment By Amount</button>
        <button onClick={() => dispatch(incrementAsync(10))}>Increment By Amount Async</button>
        <button onClick={() => dispatch(decrement())}>Decrement</button>
      </div>
    </>
  );
};

export default Counter;