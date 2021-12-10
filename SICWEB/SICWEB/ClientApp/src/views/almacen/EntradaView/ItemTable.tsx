import { useEffect, useState } from "react";
import type { FC } from "react";
import {
  Box,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
  TablePagination,
  TextField
} from "@material-ui/core";
import PerfectScrollbar from "react-perfect-scrollbar";

interface ItemTableProps {
  data: any,
  setData: Function,
  mode: boolean
}

const applyPagination = (
  data: any,
  page: number,
  limit: number
): any[] => {
  return data.slice(page * limit, page * limit + limit);
};

const ItemTable: FC<ItemTableProps> = ({ data, setData, mode }) => {

  const [page, setPage] = useState<number>(0);
  const [limit] = useState<number>(15);

  const paginatedItems = applyPagination(data, page, limit);

  const handlePageChange = (event: any, newPage: number): void => {
    setPage(newPage);
  };

  return (
    <>
      <PerfectScrollbar>
        <Box style={{ width: '100%' }}>
          <Table stickyHeader>
            <TableHead style={{ background: "red" }}>
              <TableRow>
                <TableCell>Descripción</TableCell>
                <TableCell>Can. Pedida</TableCell>
                <TableCell>Can. Recibida</TableCell>
                <TableCell>Can. Atendida</TableCell>
                <TableCell>Max. Atendida</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {paginatedItems.map((item, index) => {
                return (
                  <TableRow style={{ height: 30 }} hover key={index}>
                    <TableCell>{item.descripcion}</TableCell>
                    <TableCell>{Number(item.pedida).toFixed(2)}</TableCell>
                    <TableCell>
                      <TextField
                        id="outlined-multiline-static"
                        fullWidth
                        size="small"
                        variant="outlined"
                        onChange={(e) => {
                          let tempArr = [...data];
                          if (e.target.value === "") tempArr[index].recibida = 0;
                          else tempArr[index].recibida = Number(e.target.value);
                          setData(tempArr);
                        }}
                        type="number"
                        value={item.recibida}
                        disabled={mode}
                      />
                    </TableCell>
                    <TableCell>{Number(item.atendida).toFixed(2)}</TableCell>
                    <TableCell>{Number(item.maxatendida).toFixed(2)}</TableCell>
                  </TableRow>
                );
              })}
            </TableBody>
          </Table>
          <TablePagination
            component="div"
            count={data.length}
            onPageChange={handlePageChange}
            onRowsPerPageChange={() => { }}
            page={page}
            rowsPerPage={limit}
            rowsPerPageOptions={[15]}
          />
        </Box>
      </PerfectScrollbar>
    </>
  );
};

export default ItemTable;
