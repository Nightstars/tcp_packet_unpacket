using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace tcp_packet_unpacket
{
    class pack_unpack
    {
        /// <summary>
        /// 封包头
        /// </summary>
        /// <param name="packHead"></param>
        /// <returns></returns>
        public static byte[] PackHeadBytes(short packHead)
        {
            return BitConverter.GetBytes(packHead);
        }

        /// <summary>
        /// 封包(包含包头和包体)
        /// </summary>
        /// <param name="packHead"></param>
        /// <param name="packBody"></param>
        /// <returns></returns>
        public static byte[] PackBytes(short packHead, byte[] packBody)
        {
            byte[] headBytes = BitConverter.GetBytes(packHead);
            byte[] packBytes = new byte[headBytes.Length + packBody.Length];
            for (int i = 0; i < packBytes.Length; i++)
            {
                if (i < headBytes.Length)
                    packBytes[i] = headBytes[i];
                else
                    packBytes[i] = packBody[i - 2];
            }
            return packBytes;
        }

        /// <summary>
        /// 拆包(返回short包头,out出包体)
        /// </summary>
        /// <param name="packBytes"></param>
        /// <param name="packBody"></param>
        /// <returns></returns>
        public static short UnPack(byte[] packBytes, out byte[] packBody)
        {
            byte[] packHead = new byte[2];
            packBody = new byte[packBytes.Length - packHead.Length];
            for (int i = 0; i < packBytes.Length; i++)
            {
                if (i < 2)
                    packHead[i] = packBytes[i];
                else
                    packBody[i - 2] = packBytes[i];
            }
            short packHeadShort = BitConverter.ToInt16(packHead, 0);
            return packHeadShort;
        }

        /// <summary>
                /// 拆包头(参数只有包头)
                /// </summary>
                /// <param name="packBytes"></param>
                /// <returns></returns>
        public static short UnPackHead(byte[] packBytes)
        {
            byte[] packHead = new byte[2];
            short packHeadShort = BitConverter.ToInt16(packHead, 0);
            return packHeadShort;
        }
    }
}
