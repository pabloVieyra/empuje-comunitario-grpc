import React from "react";
import { Card, Btn } from "../../users/styles";
import { Box, Text, Flex } from "@chakra-ui/react";

const SavedEventFiltersList = ({ filters, onApply, onEdit, onDelete }) => (
  <Card style={{
    margin: "32px auto 24px auto",
    padding: "28px 36px",
    borderRadius: "36px",
    maxWidth: 1000,
    background: "#fff",
    boxShadow: "0 4px 32px #3252e715"
  }}>
    <Text fontWeight={700} fontSize="1.7rem" color="#23244b" mb={2} textAlign="center">
      Filtros guardados
    </Text>
    <Box>
      {filters.length === 0 ? (
        <Text color="#23244b" fontSize="1.1rem" textAlign="center" mt={6}>No tienes filtros guardados aún</Text>
      ) : (
        <Box as="ul" p={0} m={0} style={{ listStyle: "none" }}>
          {filters.map((f, idx) => (
            <Flex key={idx} align="center" mb={3} gap={4}>
              <Text flex={1} fontWeight={500} color="#23244b">{f.name}</Text>
              <Btn onClick={() => onApply(f)} style={{ background: "#2456e9", color: "#fff", minWidth: 90 }}>Aplicar</Btn>
              <Btn onClick={() => onEdit(f)} style={{ background: "#eee", color: "#23244b", minWidth: 90 }}>Editar</Btn>
              <Btn onClick={() => onDelete(f.name)} style={{ background: "#eee", color: "#c41e1e", minWidth: 90 }}>Eliminar</Btn>
            </Flex>
          ))}
        </Box>
      )}
    </Box>
  </Card>
);

export default SavedEventFiltersList;