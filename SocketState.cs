using System;
using System.Net.Sockets;

namespace SimpleIoT.Gateway.Models
{
    /// <summary>
    /// 비동기 소켓 통신 시 상태와 버퍼를 관리하는 객체입니다.
    /// </summary>
    public class SocketState
    {
        // 통신 데이터를 담을 버퍼
        public byte[] Buffer;
        
        // 현재 통신 중인 소켓
        // ?를 붙여 null 허용으로 변경 (CS8618 해결)
        public Socket? WorkingSocket { get; set; }
        
        // 버퍼의 크기
        public readonly int BufferSize;

        public SocketState(int bufferSize)
        {
            BufferSize = bufferSize;
            Buffer = new byte[BufferSize];
        }

        /// <summary>
        /// 다음 수신을 위해 버퍼를 초기화합니다.
        /// </summary>
        public void ClearBuffer()
        {
            Array.Clear(Buffer, 0, BufferSize);
        }
    }
}