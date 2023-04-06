import {
  Modal,
  message,
  Steps,
  theme,
  Typography,
  Button,
  Cascader,
  Checkbox,
  DatePicker,
  Form,
  Input,
  InputNumber,
  Radio,
  Select,
  Switch,
  TreeSelect,
  Upload,
} from "antd";
import { useState } from "react";
const StepPanel = (props) => {
  const [current, setCurrent] = useState(0);
  const steps = props.steps;
  const items = steps.map((item) => ({
    key: item.title,
    title: item.title,
  }));

  const { token } = theme.useToken();

  const onNext = async () => {
    try {
      await props.form.validateFields(steps[current].fields);
      setCurrent((prev) => prev + 1);
    } catch {
      (err) => console.log(err);
    }
  };

  
  const prev = () => {
    setCurrent(current - 1);
  };
  const contentStyle = {
    lineHeight: "260px",
    textAlign: "center",
    color: token.colorTextTertiary,
    backgroundColor: token.colorFillAlter,
    borderRadius: token.borderRadiusLG,
    border: `1px dashed ${token.colorBorder}`,
    marginTop: 16,
  };

  return (
    <>
      <Typography.Paragraph>
        As a SkyMiles Member, earn miles to use for flights and upgrades, plus
        enjoy Member-only offers, CLEAR® preferred pricing and more. Join today
        for free.
      </Typography.Paragraph>
      <Steps current={current} items={items} style={{marginTop:"50px"}} />
      <div style={contentStyle}>{steps[current].content}</div>
      <div
        style={{
          marginTop: 24,
        }}
      >
        {current < steps.length - 1 && (
          // <Button type="primary" onClick={() => next()}>

          <Button type="primary" onClick={() => onNext()}>
            Next
          </Button>
        )}
        {current === steps.length - 1 && (
          <Button type="primary" htmlType="submit">
            Join now
          </Button>
        )}
        {current > 0 && (
          <Button
            style={{
              margin: "0 8px",
            }}
            onClick={() => prev()}
          >
            Previous
          </Button>
        )}
      </div>
    </>
  );
};
export default StepPanel;
