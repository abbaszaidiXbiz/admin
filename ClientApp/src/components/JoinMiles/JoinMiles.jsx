import {
  Modal,
  message,
  Typography,
  Button,
  Checkbox,
  DatePicker,
  Form,
  Input,
  InputNumber,
  Radio,
  Select,
  Row,
  Col,
} from "antd";

import { DingdingOutlined } from "@ant-design/icons";
import { useState, useRef } from "react";
import StepPanel from "../StepPanel";
import OIPSkyMiles from "./../../assets/OIPSkyMiles.webp";
const JoinMiles = () => {
  const [open, setOpen] = useState(false);
  const [current, setCurrent] = useState(0);
  const formRef = useRef(null);
  const title = (
    <>
      Join SkyMiles® | Delta Air Lines <DingdingOutlined />
    </>
  );

  const [stepForm] = Form.useForm();

  const Step1Form = () => {
    return (
      <>
        <Row>
          <Col span={12}>
            <Form.Item
              name="firstName"
              label="First Name"
              rules={[
                { required: true, message: "Please enter your first name" },
              ]}
            >
              <Input />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item
              name="middleName"
              label="Middle Name"
              rules={[
                { required: true, message: "Please enter your Middle name" },
              ]}
            >
              <Input />
            </Form.Item>
          </Col>
        </Row>

        <Row>
          <Col span={12}>
            <Form.Item
              name="lastName"
              label="Last Name"
              rules={[
                { required: true, message: "Please enter your Last name" },
              ]}
            >
              <Input />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item
              name="gender"
              label="Gender"
              hasFeedback
              rules={[
                {
                  required: true,
                  message: "Please select your gender!",
                },
              ]}
            >
              <Select placeholder="Gender">
                <Option value="male"> Male </Option>
                <Option value="female"> Female </Option>
                <Option value="other"> Other </Option>
              </Select>
            </Form.Item>
          </Col>
        </Row>

        <Row>
          <Col span={12}>
            <Form.Item label="Date of Birth">
              <Form.Item name="dob">
                <DatePicker />
              </Form.Item>
            </Form.Item>
          </Col>
        </Row>
      </>
    );
  };

  const Step2Form = () => {
    return (
      <>
        <Form.Item name="field2" label="Field2">
          <Input />
        </Form.Item>
      </>
    );
  };
  const Step3Form = () => {
    return (
      <>
        <Form.Item name="field2" label="Field2">
          <Input />
        </Form.Item>
      </>
    );
  };
  const Step4Form = () => {
    return (
      <>
        <div className="scrollable-container">
          <div className="background">
            <Typography.Title level={3}>Terms and conditions</Typography.Title>
            <Typography.Paragraph copyable={false}>
              *Basic Economy tickets purchased on or after December 9, 2021 for
              flights departing on or after January 1, 2022, are not eligible to
              earn miles in the SkyMiles Program or earn towards Medallion
              Status (meaning not eligible for MQM, MQS or MQD earn). All
              SkyMiles program rules apply. See Membership Guide & Program Rules
              for details. Taxes and fees for Award Travel are the
              responsibility of the passenger and must be paid at the time the
              ticket is booked. Award Travel seats are limited and may not be
              available on all flights or in all markets. Partner offers subject
              to the terms and conditions of each individual offer. Offers void
              where prohibited by law. Other restrictions may apply. United
              States residents will be opted-in to receive e-mails about our
              product and services. You can manage your email subscription
              preferences at any time in your SkyMiles account profile.
            </Typography.Paragraph>
          </div>
        </div>
        <Form.Item
          name="agree"
          valuePropName="checked"
          rules={[{ required: true, message: "Please agree to T&Cs." }]}
        >
          <Checkbox>I agree to the Terms&Conditions</Checkbox>
        </Form.Item>
      </>
    );
  };

  const steps = [
    {
      title: "Basic Info",
      content: <Step1Form />,
      fields: ["name", "surname", "type"],
    },
    {
      title: "Contact Info",
      content: <Step2Form />,
      fields: [],
    },
    {
      title: "Login Info",
      content: <Step3Form />,
      fields: ["agree"],
    },
    {
      title: "T&Cs",
      content: <Step4Form />,
      fields: ["agree"],
    },
  ];

  const formItemLayout = {
    labelCol: {
      span: 6,
    },
  };
  const onFinish = (fieldsValue) => {
    const formData = stepForm.getFieldsValue(true);

    // POST the data to backend and show Notification
    console.log(formData);
    Modal.destroyAll()
  };

  const onFinishFailed = (errorInfo) => {
    const formData = stepForm.getFieldsError(true);
    console.log(formData);
  };

  const onReset = () => {
    formRef.current?.resetFields();
    setOpen(false)
  };

  return (
    <>
      <Button type="primary" block danger onClick={() => setOpen(true)}>
        Join Sky Miles
      </Button>
      <Modal
       destroyOnClose={true}
        title={title}
        centered
        open={open}
        onOk={() => setOpen(false)}
        onCancel={() => onReset()}
        width={1000}
        cancelButtonProps={{ style: { display: "none" } }}
        okButtonProps={{ style: { display: "none" } }}
      >
        <>
          <Form
            form={stepForm}
            ref={formRef}
            {...formItemLayout}
            onFinish={onFinish}
            onFinishFailed={onFinishFailed}
          >
            <StepPanel steps={steps} form={stepForm} />
          </Form>
        </>
      </Modal>
    </>
  );
};
export default JoinMiles;
