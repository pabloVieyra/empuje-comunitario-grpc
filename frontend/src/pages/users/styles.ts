import styled from "styled-components";

export const UsersPage = styled.div`
  min-height: 100vh;
  background: linear-gradient(120deg, #f4f6fb 60%, #1a1c25 100%);
  padding: 40px;
  display: flex;
  flex-direction: column;
  align-items: center;
`;

export const Card = styled.div`
  background: #fff;
  border-radius: 30px;
  box-shadow: 0 8px 48px #1a1c2512, 0 1.5px 18px #3252e710;
  padding: 38px 26px;
  width: 100%;
  max-width: 1100px;
  margin-bottom: 32px;
  display: flex;
  flex-direction: column;
  gap: 38px;
`;

export const Table = styled.table`
  width: 100%;
  border-spacing: 0;
  border-radius: 18px;
  background: #fff;
  box-shadow: 0 2px 18px #1a1c2512;
  overflow: hidden;
  font-size: 1.08rem;
  th, td {
    padding: 17px 12px;
    text-align: left;
    border-bottom: 1px solid #f0f1f7;
  }
  th {
    background: #f6f8fc;
    font-weight: 700;
    font-size: 1.04rem;
    letter-spacing: 0.01em;
    color: #23244b;
  }
  tr:last-child td {
    border-bottom: none;
  }
`;

export const Btn = styled.button`
  background: linear-gradient(90deg, #3252e7 0%, #4f66fa 100%);
  color: #fff;
  border: none;
  border-radius: 14px;
  padding: 12px 26px;
  margin: 0 8px 8px 0;
  font-weight: 600;
  letter-spacing: 0.025em;
  cursor: pointer;
  font-size: 1.08rem;
  box-shadow: 0 2px 16px #3252e730;
  transition: 0.18s;
  &:hover { filter: brightness(1.07); box-shadow: 0 4px 20px #3252e730; }
`;

export const ModalBg = styled.div`
  position: fixed; top:0; left:0; width:100vw; height:100vh;
  background: rgba(50,82,231,0.09);
  display: flex; align-items: center; justify-content: center;
  z-index: 9999;
  backdrop-filter: blur(2px);
`;

export const ModalCard = styled.div`
  background: #fff;
  border-radius: 24px;
  padding: 36px 28px;
  min-width: 340px;
  max-width: 96vw;
  box-shadow: 0 4px 48px #3252e74a;
  display: flex;
  flex-direction: column;
`;

export const ModalTitle = styled.h3`
  font-size: 2rem;
  margin-bottom: 22px;
  font-weight: 700;
  color: #23244b;
`;

export const ModalFooter = styled.div`
  display: flex;
  gap: 18px;
  margin-top: 24px;
  justify-content: flex-end;
`;

export const ErrorMsg = styled.div`
  color: #ff2f2f;
  font-weight: 600;
  margin-top: 12px;
`;

export const Input = styled.input`
  width: 100%;
  padding: 11px 14px;
  border-radius: 10px;
  border: 1.6px solid #e4e8f2;
  background: #f9fbff;
  font-size: 1rem;
  margin-bottom: 18px;
  transition: border 0.16s;
  &:focus {
    border-color: #3252e7;
    outline: none;
    background: #f4f8ff;
  }
`;

export const Select = styled.select`
  width: 100%;
  padding: 11px 14px;
  border-radius: 10px;
  border: 1.6px solid #e4e8f2;
  background: #f9fbff;
  font-size: 1rem;
  margin-bottom: 18px;
  transition: border 0.16s;
  &:focus {
    border-color: #3252e7;
    outline: none;
    background: #f4f8ff;
  }
`;