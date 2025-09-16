import { Layout, LeftPanel, OuterWrapper, RightPanel, Visual } from "../styles";

export const LoginLayout: React.FC<{ children: React.ReactNode }> = ({ children }) => (
  <OuterWrapper>
    <Layout>
      <LeftPanel>{children}</LeftPanel>
      <RightPanel>
        <Visual />
      </RightPanel>
    </Layout>
  </OuterWrapper>
);